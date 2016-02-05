using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using eSIS.Core;
using eSIS.Core.Entities;
using eSIS.Core.Entities.Infrastructure;
using eSIS.Core.Exceptions;
using NLog;

namespace eSIS.Database
{
    public class SisContext : DbContext
    {
        private readonly Logger _logger;
        private bool _debugMode;
        private readonly string _userName;
        private readonly string _ipAddress;

        //This is required by EF migrations
        public SisContext()
            : base(ConfigurationHelper.InstanceDbConnectionName)
        { }

        public SisContext(string userName, string iPAddress)
            : this()
        {
            _logger = LogManager.GetLogger("SisContext");
            _logger.Trace("Context starting");
            _userName = userName;
            _ipAddress = iPAddress;

            if (ConfigurationHelper.InstanceDbIsLogging)
            {
                var dbLogger = new DatabaseLogger();
                dbLogger.StartLogging();
            }
        }

#if DEBUG

        /// <summary>
        /// Outputs Database calls to Debugging console
        /// </summary>
        public bool DebugMode
        {
            get { return _debugMode; }
            set
            {
                _debugMode = value;
                Database.Log = _debugMode ? (Action<string>)(s => Debug.WriteLine(s)) : null;
            }
        }

#endif

        public bool AuditChanges { get; set; } = true;

        #region Core tables

        public DbSet<DataAudit> DataHistory { get; set; }

        public DbSet<DataAuditDetail> DataHistoryDetails { get; set; }

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
        public DbSet<Course> Courses { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<EnrollmentCode> EnrollmentCodes { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolType> SchoolTypes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().MapToStoredProcedures(sp => sp.Insert(i => i.HasName("usp_LogInsert"))); //this is so that create can be called by our logging framework
            modelBuilder.Entity<Grade>().HasOptional(p => p.NextGrade).WithMany().HasForeignKey(p => p.NextGradeId);
            modelBuilder.Entity<Grade>().HasOptional(p => p.PreviousGrade).WithMany().HasForeignKey(p => p.PreviousGradeId);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync()
        {
            _logger.Trace("Starting custom SaveChanges");

            if (string.IsNullOrWhiteSpace(_userName))
            {
                _logger.Warn("No value for _userName");
                throw new Exception("Username MUST have a value!");
            }

            if (string.IsNullOrWhiteSpace(_ipAddress))
            {
                _logger.Warn("No value for _ipAddress");
                throw new Exception("User Ip Address MUST have a value!");
            }

            try
            {
                var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
                var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();

                foreach (var entry in addedEntries)
                {
                    var addUserProperty = entry.Property("AddUser");
                    var addDateProeprty = entry.Property("AddDate");

                    addUserProperty.CurrentValue = _userName;
                    addDateProeprty.CurrentValue = DateTime.Now;
                }

                foreach (var entry in modifiedEntries)
                {
                    var modUserProperty = entry.Property("ModUser");
                    var modDateProperty = entry.Property("ModDate");

                    modUserProperty.CurrentValue = _userName;
                    modDateProperty.CurrentValue = DateTime.Now;
                }

                _logger.Trace("Starting base save changes");

                return AuditChanges 
                    ? await SaveChangesWithAudit() 
                    : await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                _logger.Trace("DbEntityValidationException caught");

                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " - ", fullErrorMessage);

                throw new ValidationException(exceptionMessage);
            }
        }

        private async Task<int> SaveChangesWithAudit()
        {
            _logger.Trace("Starting audit save changes");

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

                _logger.Trace("Starting base save changes");

                await base.SaveChangesAsync();

                _logger.Trace("Completing audit transaction");
                scope.Complete();
                return changes;
            }
        }

        #region Data Audit

        private void AuditCreate(DbEntityEntry entry)
        {
            var history = new DataAudit
            {
                Action = "Create",
                Table = GetTableName(entry),
                Timestamp = DateTime.Now,
                RecordId = entry.CurrentValues.GetValue<int>("Id"),
                UserName = _userName,
                UserIpAddress = _ipAddress,
                AddUser = _userName,
                AddDate = DateTime.Now
            };

            foreach (var prop in entry.CurrentValues.PropertyNames)
            {
                var value = entry.CurrentValues.GetValue<object>(prop);
                if (value != null)
                {
                    var detail = new DataAuditDetail
                    {
                        FieldName = prop,
                        BeforeValue = null,
                        AfterValue = value.ToString(),
                        AddUser = _userName,
                        AddDate = DateTime.Now
                    };
                    history.Details.Add(detail);
                }
            }

            DataHistory.Add(history);
        }

        private void AuditModify(DbEntityEntry entry)
        {
            var history = new DataAudit
            {
                Action = "Update",
                Table = GetTableName(entry),
                Timestamp = DateTime.Now,
                RecordId = entry.CurrentValues.GetValue<int>("Id"),
                UserName = _userName,
                UserIpAddress = _ipAddress,
                AddUser = _userName,
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
                    var detail = new DataAuditDetail
                    {
                        FieldName = prop,
                        BeforeValue = oldValue.ToString(),
                        AfterValue = currentValue.ToString(),
                        AddUser = _userName,
                        AddDate = DateTime.Now
                    };
                    history.Details.Add(detail);
                }
            }

            DataHistory.Add(history);
        }

        private void AuditDelete(DbEntityEntry entry)
        {
            var history = new DataAudit
            {
                Action = "Delete",
                Table = GetTableName(entry),
                Timestamp = DateTime.Now,
                RecordId = entry.OriginalValues.GetValue<int>("Id"),
                UserName = _userName,
                UserIpAddress = _ipAddress,
                AddUser = _userName,
                AddDate = DateTime.Now
            };

            foreach (var prop in entry.OriginalValues.PropertyNames)
            {
                var value = entry.OriginalValues.GetValue<object>(prop);
                if (value != null)
                {
                    var detail = new DataAuditDetail
                    {
                        FieldName = prop,
                        BeforeValue = value.ToString(),
                        AfterValue = null,
                        AddUser = _userName,
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

        #endregion
    }
}
