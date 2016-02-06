namespace eSIS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstRun : DbMigration
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
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.Announcement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false, maxLength: 500),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.DataAudit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(nullable: false),
                        Table = c.String(nullable: false),
                        RecordId = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        UserIpAddress = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.DataAuditDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataAuditId = c.Int(nullable: false),
                        FieldName = c.String(nullable: false, maxLength: 50),
                        BeforeValue = c.String(nullable: false, maxLength: 100),
                        AfterValue = c.String(nullable: false, maxLength: 100),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("inf.DataAudit", t => t.DataAuditId)
                .Index(t => t.DataAuditId)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "sis.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 45),
                        AddressId = c.Int(nullable: false),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sis.Address", t => t.AddressId)
                .Index(t => t.AddressId)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "sis.School",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 45),
                        AddressId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        SchoolTypeId = c.Int(nullable: false),
                        SystemCode = c.String(maxLength: 10),
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
                .Index(t => t.AddressId)
                .Index(t => t.DistrictId)
                .Index(t => t.SchoolTypeId)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "sis.SchoolType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 45),
                        AllowCustomGradingScales = c.Boolean(nullable: false),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "sis.Grade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 45),
                        PreviousGradeId = c.Int(),
                        NextGradeId = c.Int(),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sis.Grade", t => t.NextGradeId)
                .ForeignKey("sis.Grade", t => t.PreviousGradeId)
                .Index(t => t.PreviousGradeId)
                .Index(t => t.NextGradeId)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MachineName = c.String(maxLength: 200),
                        SiteName = c.String(nullable: false, maxLength: 200),
                        Logged = c.DateTime(nullable: false),
                        Level = c.String(nullable: false, maxLength: 5),
                        UserName = c.String(maxLength: 200),
                        Message = c.String(nullable: false),
                        Logger = c.String(maxLength: 300),
                        Properties = c.String(),
                        ServerName = c.String(maxLength: 200),
                        Port = c.String(maxLength: 100),
                        Url = c.String(maxLength: 2000),
                        Https = c.Boolean(),
                        ServerAddress = c.String(maxLength: 100),
                        RemoteAddress = c.String(maxLength: 100),
                        Callstie = c.String(maxLength: 300),
                        Exception = c.String(),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.LookUp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 1000),
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
            
            CreateTable(
                "inf.ResetQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false, maxLength: 2000),
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
            
            CreateTable(
                "inf.SystemSetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Value = c.String(nullable: false, maxLength: 1000),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.UserPassword",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Password = c.Binary(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("inf.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.User",
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
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("inf.ResetQuestion", t => t.ResetQuestion1Id)
                .ForeignKey("inf.ResetQuestion", t => t.ResetQuestion2Id)
                .Index(t => t.ResetQuestion1Id)
                .Index(t => t.ResetQuestion2Id)
                .Index(t => t.SystemCode);
            
            CreateTable(
                "inf.UserSalt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Salt = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        SystemCode = c.String(maxLength: 10),
                        AddUser = c.String(maxLength: 60),
                        AddDate = c.DateTime(),
                        ModUser = c.String(maxLength: 60),
                        ModDate = c.DateTime(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("inf.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SystemCode);
            
            CreateStoredProcedure(
                "inf.usp_LogInsert",
                p => new
                    {
                        MachineName = p.String(maxLength: 200),
                        SiteName = p.String(maxLength: 200),
                        Logged = p.DateTime(),
                        Level = p.String(maxLength: 5),
                        UserName = p.String(maxLength: 200),
                        Message = p.String(),
                        Logger = p.String(maxLength: 300),
                        Properties = p.String(),
                        ServerName = p.String(maxLength: 200),
                        Port = p.String(maxLength: 100),
                        Url = p.String(maxLength: 2000),
                        Https = p.Boolean(),
                        ServerAddress = p.String(maxLength: 100),
                        RemoteAddress = p.String(maxLength: 100),
                        Callstie = p.String(maxLength: 300),
                        Exception = p.String(),
                        SystemCode = p.String(maxLength: 10),
                        AddUser = p.String(maxLength: 60),
                        AddDate = p.DateTime(),
                        ModUser = p.String(maxLength: 60),
                        ModDate = p.DateTime(),
                    },
                body:
                    @"INSERT [inf].[Log]([MachineName], [SiteName], [Logged], [Level], [UserName], [Message], [Logger], [Properties], [ServerName], [Port], [Url], [Https], [ServerAddress], [RemoteAddress], [Callstie], [Exception], [SystemCode], [AddUser], [AddDate], [ModUser], [ModDate])
                      VALUES (@MachineName, @SiteName, @Logged, @Level, @UserName, @Message, @Logger, @Properties, @ServerName, @Port, @Url, @Https, @ServerAddress, @RemoteAddress, @Callstie, @Exception, @SystemCode, @AddUser, @AddDate, @ModUser, @ModDate)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [inf].[Log]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id], t0.[Version]
                      FROM [inf].[Log] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "inf.usp_LogUpdate",
                p => new
                    {
                        Id = p.Int(),
                        MachineName = p.String(maxLength: 200),
                        SiteName = p.String(maxLength: 200),
                        Logged = p.DateTime(),
                        Level = p.String(maxLength: 5),
                        UserName = p.String(maxLength: 200),
                        Message = p.String(),
                        Logger = p.String(maxLength: 300),
                        Properties = p.String(),
                        ServerName = p.String(maxLength: 200),
                        Port = p.String(maxLength: 100),
                        Url = p.String(maxLength: 2000),
                        Https = p.Boolean(),
                        ServerAddress = p.String(maxLength: 100),
                        RemoteAddress = p.String(maxLength: 100),
                        Callstie = p.String(maxLength: 300),
                        Exception = p.String(),
                        SystemCode = p.String(maxLength: 10),
                        AddUser = p.String(maxLength: 60),
                        AddDate = p.DateTime(),
                        ModUser = p.String(maxLength: 60),
                        ModDate = p.DateTime(),
                        Version_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"UPDATE [inf].[Log]
                      SET [MachineName] = @MachineName, [SiteName] = @SiteName, [Logged] = @Logged, [Level] = @Level, [UserName] = @UserName, [Message] = @Message, [Logger] = @Logger, [Properties] = @Properties, [ServerName] = @ServerName, [Port] = @Port, [Url] = @Url, [Https] = @Https, [ServerAddress] = @ServerAddress, [RemoteAddress] = @RemoteAddress, [Callstie] = @Callstie, [Exception] = @Exception, [SystemCode] = @SystemCode, [AddUser] = @AddUser, [AddDate] = @AddDate, [ModUser] = @ModUser, [ModDate] = @ModDate
                      WHERE (([Id] = @Id) AND (([Version] = @Version_Original) OR ([Version] IS NULL AND @Version_Original IS NULL)))
                      
                      SELECT t0.[Version]
                      FROM [inf].[Log] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "inf.usp_LogDelete",
                p => new
                    {
                        Id = p.Int(),
                        Version_Original = p.Binary(maxLength: 8, fixedLength: true, storeType: "rowversion"),
                    },
                body:
                    @"DELETE [inf].[Log]
                      WHERE (([Id] = @Id) AND (([Version] = @Version_Original) OR ([Version] IS NULL AND @Version_Original IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("inf.usp_LogDelete");
            DropStoredProcedure("inf.usp_LogUpdate");
            DropStoredProcedure("inf.usp_LogInsert");
            DropForeignKey("inf.UserSalt", "UserId", "inf.User");
            DropForeignKey("inf.UserPassword", "UserId", "inf.User");
            DropForeignKey("inf.User", "ResetQuestion2Id", "inf.ResetQuestion");
            DropForeignKey("inf.User", "ResetQuestion1Id", "inf.ResetQuestion");
            DropForeignKey("sis.Grade", "PreviousGradeId", "sis.Grade");
            DropForeignKey("sis.Grade", "NextGradeId", "sis.Grade");
            DropForeignKey("sis.School", "SchoolTypeId", "sis.SchoolType");
            DropForeignKey("sis.School", "DistrictId", "sis.District");
            DropForeignKey("sis.School", "AddressId", "sis.Address");
            DropForeignKey("sis.District", "AddressId", "sis.Address");
            DropForeignKey("inf.DataAuditDetail", "DataAuditId", "inf.DataAudit");
            DropIndex("inf.UserSalt", new[] { "SystemCode" });
            DropIndex("inf.UserSalt", new[] { "UserId" });
            DropIndex("inf.User", new[] { "SystemCode" });
            DropIndex("inf.User", new[] { "ResetQuestion2Id" });
            DropIndex("inf.User", new[] { "ResetQuestion1Id" });
            DropIndex("inf.UserPassword", new[] { "SystemCode" });
            DropIndex("inf.UserPassword", new[] { "UserId" });
            DropIndex("inf.SystemSetting", new[] { "SystemCode" });
            DropIndex("inf.ResetQuestion", new[] { "SystemCode" });
            DropIndex("inf.LookUp", new[] { "SystemCode" });
            DropIndex("inf.Log", new[] { "SystemCode" });
            DropIndex("sis.Grade", new[] { "SystemCode" });
            DropIndex("sis.Grade", new[] { "NextGradeId" });
            DropIndex("sis.Grade", new[] { "PreviousGradeId" });
            DropIndex("sis.SchoolType", new[] { "SystemCode" });
            DropIndex("sis.School", new[] { "SystemCode" });
            DropIndex("sis.School", new[] { "SchoolTypeId" });
            DropIndex("sis.School", new[] { "DistrictId" });
            DropIndex("sis.School", new[] { "AddressId" });
            DropIndex("sis.District", new[] { "SystemCode" });
            DropIndex("sis.District", new[] { "AddressId" });
            DropIndex("inf.DataAuditDetail", new[] { "SystemCode" });
            DropIndex("inf.DataAuditDetail", new[] { "DataAuditId" });
            DropIndex("inf.DataAudit", new[] { "SystemCode" });
            DropIndex("inf.Announcement", new[] { "SystemCode" });
            DropIndex("sis.Address", new[] { "SystemCode" });
            DropTable("inf.UserSalt");
            DropTable("inf.User");
            DropTable("inf.UserPassword");
            DropTable("inf.SystemSetting");
            DropTable("inf.ResetQuestion");
            DropTable("inf.LookUp");
            DropTable("inf.Log");
            DropTable("sis.Grade");
            DropTable("sis.SchoolType");
            DropTable("sis.School");
            DropTable("sis.District");
            DropTable("inf.DataAuditDetail");
            DropTable("inf.DataAudit");
            DropTable("inf.Announcement");
            DropTable("sis.Address");
        }
    }
}
