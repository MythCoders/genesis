namespace eSIS.Database.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "sis.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(maxLength: 60),
                        City = c.String(maxLength: 45),
                        State = c.String(maxLength: 2),
                        ZipCode = c.String(maxLength: 9),
                        PhoneNumber = c.String(maxLength: 10),
                        AlternatePhoneNumber = c.String(maxLength: 10),
                        EmailAddress = c.String(maxLength: 60),
                        AlternateEmailAddress = c.String(maxLength: 60),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Announcement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false, maxLength: 500),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.DataHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(nullable: false),
                        LoggedTable = c.String(nullable: false),
                        Key = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        UserIpAddress = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.DataHistoryDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataHistoryId = c.Int(nullable: false),
                        FieldName = c.String(nullable: false, maxLength: 50),
                        BeforeValue = c.String(nullable: false, maxLength: 100),
                        AfterValue = c.String(nullable: false, maxLength: 100),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.DataHistory", t => t.DataHistoryId)
                .Index(t => t.DataHistoryId);
            
            CreateTable(
                "sis.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemCode = c.String(maxLength: 5),
                        Name = c.String(maxLength: 45),
                        AddressId = c.Int(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sis.Address", t => t.AddressId)
                .Index(t => t.SystemCode, unique: true, name: "IX_DistrictSystemCode")
                .Index(t => t.AddressId);
            
            CreateTable(
                "sis.School",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemCode = c.String(maxLength: 5),
                        Name = c.String(maxLength: 45),
                        AddressId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        SchoolTypeId = c.Int(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sis.Address", t => t.AddressId)
                .ForeignKey("sis.District", t => t.DistrictId)
                .ForeignKey("sis.SchoolType", t => t.SchoolTypeId)
                .Index(t => t.SystemCode, unique: true, name: "IX_SchoolSystemCode")
                .Index(t => t.AddressId)
                .Index(t => t.DistrictId)
                .Index(t => t.SchoolTypeId);
            
            CreateTable(
                "sis.SchoolType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemCode = c.String(maxLength: 5),
                        Name = c.String(maxLength: 45),
                        AllowCustomGradingScales = c.Boolean(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode, unique: true, name: "IX_SchoolTypeSystemCode");
            
            CreateTable(
                "sis.Grade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemCode = c.String(maxLength: 5),
                        Name = c.String(maxLength: 45),
                        PreviousGradeId = c.Int(),
                        NextGradeId = c.Int(),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sis.Grade", t => t.NextGradeId)
                .ForeignKey("sis.Grade", t => t.PreviousGradeId)
                .Index(t => t.SystemCode, unique: true, name: "IX_GradeSystemCode")
                .Index(t => t.PreviousGradeId)
                .Index(t => t.NextGradeId);
            
            CreateTable(
                "core.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        Source = c.String(),
                        UserName = c.String(),
                        IpAddress = c.String(),
                        IsError = c.Boolean(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.LookUp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.ResetQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false, maxLength: 2000),
                        IsActive = c.Boolean(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.SystemSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 1000),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.UserPassword",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Password = c.Binary(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "core.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(nullable: false),
                        AccessToken = c.String(),
                        AccessTokenCreateDate = c.DateTime(nullable: false),
                        AccessTokenIsUsed = c.Boolean(nullable: false),
                        ResetQuestion1Id = c.Int(),
                        ResetQuestion2Id = c.Int(),
                        ResetQuestion1Answer = c.String(nullable: false),
                        ResetQuestion2Answer = c.String(nullable: false),
                        ActivationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        AccountStatus = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.ResetQuestion", t => t.ResetQuestion1Id)
                .ForeignKey("core.ResetQuestion", t => t.ResetQuestion2Id)
                .Index(t => t.ResetQuestion1Id)
                .Index(t => t.ResetQuestion2Id);
            
            CreateTable(
                "core.UserSalt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Salt = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.UserSalt", "UserId", "core.User");
            DropForeignKey("core.UserPassword", "UserId", "core.User");
            DropForeignKey("core.User", "ResetQuestion2Id", "core.ResetQuestion");
            DropForeignKey("core.User", "ResetQuestion1Id", "core.ResetQuestion");
            DropForeignKey("sis.Grade", "PreviousGradeId", "sis.Grade");
            DropForeignKey("sis.Grade", "NextGradeId", "sis.Grade");
            DropForeignKey("sis.School", "SchoolTypeId", "sis.SchoolType");
            DropForeignKey("sis.School", "DistrictId", "sis.District");
            DropForeignKey("sis.School", "AddressId", "sis.Address");
            DropForeignKey("sis.District", "AddressId", "sis.Address");
            DropForeignKey("core.DataHistoryDetail", "DataHistoryId", "core.DataHistory");
            DropIndex("core.UserSalt", new[] { "UserId" });
            DropIndex("core.User", new[] { "ResetQuestion2Id" });
            DropIndex("core.User", new[] { "ResetQuestion1Id" });
            DropIndex("core.UserPassword", new[] { "UserId" });
            DropIndex("sis.Grade", new[] { "NextGradeId" });
            DropIndex("sis.Grade", new[] { "PreviousGradeId" });
            DropIndex("sis.Grade", "IX_GradeSystemCode");
            DropIndex("sis.SchoolType", "IX_SchoolTypeSystemCode");
            DropIndex("sis.School", new[] { "SchoolTypeId" });
            DropIndex("sis.School", new[] { "DistrictId" });
            DropIndex("sis.School", new[] { "AddressId" });
            DropIndex("sis.School", "IX_SchoolSystemCode");
            DropIndex("sis.District", new[] { "AddressId" });
            DropIndex("sis.District", "IX_DistrictSystemCode");
            DropIndex("core.DataHistoryDetail", new[] { "DataHistoryId" });
            DropTable("core.UserSalt");
            DropTable("core.User");
            DropTable("core.UserPassword");
            DropTable("core.SystemSetting");
            DropTable("core.ResetQuestion");
            DropTable("core.LookUp");
            DropTable("core.Log");
            DropTable("sis.Grade");
            DropTable("sis.SchoolType");
            DropTable("sis.School");
            DropTable("sis.District");
            DropTable("core.DataHistoryDetail");
            DropTable("core.DataHistory");
            DropTable("core.Announcement");
            DropTable("sis.Address");
        }
    }
}
