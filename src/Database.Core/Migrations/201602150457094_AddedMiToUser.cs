namespace eSIS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMiToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("inf.User", "MI", c => c.String(maxLength: 2));
            AlterColumn("inf.User", "EmailAddress", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("inf.User", "FirstName", c => c.String(nullable: false, maxLength: 35));
            AlterColumn("inf.User", "LastName", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("inf.User", "LastName", c => c.String(nullable: false));
            AlterColumn("inf.User", "FirstName", c => c.String(nullable: false));
            AlterColumn("inf.User", "EmailAddress", c => c.String(nullable: false));
            DropColumn("inf.User", "MI");
        }
    }
}
