namespace eSIS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserRequiredField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("inf.User", "AccessTokenCreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("inf.User", "AccessTokenCreateDate", c => c.DateTime(nullable: false));
        }
    }
}
