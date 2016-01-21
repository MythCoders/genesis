using System;
using eSIS.Database.Core.Entities;
using System.Data.Entity.Migrations;

namespace eSIS.Database.Core.Migrations
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
