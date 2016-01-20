using System.Data.Entity;
using eSIS.Database.Core.Entities;

namespace eSIS.Database.Core
{
    public class SisContext : CustomDbContext
    {
        public SisContext(bool debugMode = false)
            : base(debugMode)
        { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}
