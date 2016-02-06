namespace eSIS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("inf.DataAuditDetail", "BeforeValue", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("inf.DataAuditDetail", "BeforeValue", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
