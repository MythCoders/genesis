using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using JA.eSIS.DB.Database.Entities;
using JA.eSIS.DB.Database.Entities.Core;

namespace JA.eSIS.DB.Database
{
    public class SisContext : DbContext
    {
        public string UserName;

        public string UserIpAddress;

        public bool AuditChanges { get; set; }

        public SisContext(bool debugMode, string dbName)
            : base($"name={dbName}")
        {
            if (debugMode)
            {
                Database.Log = s => Debug.WriteLine(s);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        #region Base Tables

        public DbSet<DataHistory> DataHistory { get; set; }

        public DbSet<DataHistoryDetail> DataHistoryDetails { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserPassword> UserPasswords { get; set; }

        public DbSet<UserSalt> UserSalts { get; set; }

        public DbSet<ResetQuestion> ResetQuestions { get; set; }

        public DbSet<SystemSetting> SystemSettings { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<LookUp> LookUps { get; set; }

        #endregion 

        public DbSet<Address> Addresses { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Grade> Grades { get; set; } 

        public override int SaveChanges()
        {
            if (UserName == null)
            {
                UserName = "Unknown";
            }

            if (UserIpAddress == null)
            {
                UserIpAddress = "Unknown";
            }

            try
            {
                var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
                var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();

                foreach (var entry in addedEntries)
                {
                    var addUserProperty = entry.Property("AddUser");

                    if (addUserProperty != null)
                    {
                        addUserProperty.CurrentValue = UserName;
                    }

                    var addDateProeprty = entry.Property("AddDate");

                    if (addDateProeprty != null)
                    {
                        addDateProeprty.CurrentValue = DateTime.Now;
                    }
                }

                foreach (var entry in modifiedEntries)
                {
                    var modUserProperty = entry.Property("ModUser");

                    if (modUserProperty != null)
                    {
                        modUserProperty.CurrentValue = UserName;
                    }

                    var modDateProperty = entry.Property("ModDate");

                    if (modDateProperty != null)
                    {
                        modDateProperty.CurrentValue = DateTime.Now;
                    }
                }

                return AuditChanges ? SaveChangesWithAudit() : base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " - ", fullErrorMessage);

                throw new Exception(exceptionMessage);
            }
        }

        private int SaveChangesWithAudit()
        {
            using (var scope = new TransactionScope())
            {
                var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
                var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
                var deletedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted).ToList();

                foreach (var entry in deletedEntries)
                {
                    AuditDelete(entry);
                }

                foreach (var entry in modifiedEntries)
                {
                    AuditModify(entry);
                }

                var changes = base.SaveChanges();
                foreach (var entry in addedEntries)
                {
                    AuditCreate(entry);
                }

                base.SaveChanges();
                scope.Complete();
                return changes;
            }
        }

        private void AuditCreate(DbEntityEntry entry)
        {
            var history = new DataHistory
            {
                Action = "Create",
                LoggedTable = GetTableName(entry),
                Timestamp = DateTime.Now,
                Key = entry.CurrentValues.GetValue<int>("ID"),
                UserName = UserName,
                UserIpAddress = UserIpAddress,
                AddUser = UserName,
                AddDate = DateTime.Now
            };

            foreach (var prop in entry.CurrentValues.PropertyNames)
            {
                var value = entry.CurrentValues.GetValue<object>(prop);
                if (value != null)
                {
                    var detail = new DataHistoryDetail
                    {
                        FieldName = prop,
                        BeforeValue = null,
                        AfterValue = value.ToString(),
                        AddUser = UserName,
                        AddDate = DateTime.Now
                    };
                    history.Details.Add(detail);
                }
            }

            DataHistory.Add(history);
        }

        private void AuditModify(DbEntityEntry entry)
        {
            var history = new DataHistory
            {
                Action = "Update",
                LoggedTable = GetTableName(entry),
                Timestamp = DateTime.Now,
                Key = entry.CurrentValues.GetValue<int>("ID"),
                UserName = UserName,
                UserIpAddress = UserIpAddress,
                AddUser = UserName,
                AddDate = DateTime.Now
            };

            foreach (var prop in entry.CurrentValues.PropertyNames)
            {
                var currentValue = entry.CurrentValues.GetValue<object>(prop);
                var oldValue = entry.OriginalValues.GetValue<object>(prop);
                currentValue = currentValue?.ToString() ?? string.Empty;

                oldValue = oldValue?.ToString() ?? string.Empty;

                if (currentValue.ToString() != oldValue.ToString())
                {
                    var detail = new DataHistoryDetail
                    {
                        FieldName = prop,
                        BeforeValue = oldValue.ToString(),
                        AfterValue = currentValue.ToString(),
                        AddUser = UserName,
                        AddDate = DateTime.Now
                    };
                    history.Details.Add(detail);
                }
            }

            DataHistory.Add(history);
        }

        private void AuditDelete(DbEntityEntry entry)
        {
            var history = new DataHistory
            {
                Action = "Delete",
                LoggedTable = GetTableName(entry),
                Timestamp = DateTime.Now,
                Key = entry.OriginalValues.GetValue<int>("ID"),
                UserName = UserName,
                UserIpAddress = UserIpAddress,
                AddUser = UserName,
                AddDate = DateTime.Now
            };

            foreach (var prop in entry.OriginalValues.PropertyNames)
            {
                var value = entry.OriginalValues.GetValue<object>(prop);
                if (value != null)
                {
                    var detail = new DataHistoryDetail
                    {
                        FieldName = prop,
                        BeforeValue = value.ToString(),
                        AfterValue = null,
                        AddUser = UserName,
                        AddDate = DateTime.Now
                    };
                    history.Details.Add(detail);
                }
            }
            DataHistory.Add(history);
        }

        private string GetTableName(DbEntityEntry ent)
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            var entityType = ent.Entity.GetType();

            if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
            {
                entityType = entityType.BaseType;
            }

            var entityTypeName = entityType.Name;
            var container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
            return container.BaseEntitySets.Where(meta => meta.ElementType.Name == entityTypeName).Select(meta => meta.Name).First();
        }
    }
}
