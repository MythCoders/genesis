using System.Data.Entity;
using eSIS.Database.Core.Entities;

namespace eSIS.Database.Core
{
    public class SisContext : CustomDbContext
    {
        //This is required for Ef Migrations
        public SisContext()
            // ReSharper disable once RedundantArgumentDefaultValue
            : this(false)
        { }

        // ReSharper disable once MemberCanBePrivate.Global
        public SisContext(bool debugMode = false)
            : base(debugMode)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>().HasOptional(p => p.NextGrade).WithMany().HasForeignKey(p => p.NextGradeId);
            modelBuilder.Entity<Grade>().HasOptional(p => p.PreviousGrade).WithMany().HasForeignKey(p => p.PreviousGradeId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolType> SchoolTypes { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
