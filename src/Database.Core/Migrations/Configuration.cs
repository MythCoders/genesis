using System.Data.Entity.Migrations;

namespace eSIS.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SisContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SisContext context)
        {
            
        }
    }
}
