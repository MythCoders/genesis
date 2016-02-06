namespace eSIS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStProcs : DbMigration
    {
        public override void Up()
        {
            DropStoredProcedure("inf.usp_LogInsert");
            DropStoredProcedure("inf.usp_LogUpdate");
            DropStoredProcedure("inf.usp_LogDelete");
        }
        
        public override void Down()
        {
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
