namespace eSIS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiClientInDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "inf.ApiClient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Token = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode);
            
        }
        
        public override void Down()
        {
            DropIndex("inf.ApiClient", new[] { "SystemCode" });
            DropTable("inf.ApiClient");
        }
    }
}
