namespace eSIS.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpandAddresses : DbMigration
    {
        public override void Up()
        {
            AddColumn("sis.Address", "FirstAddressLine", c => c.String(maxLength: 60));
            AddColumn("sis.Address", "SecondAddressLine", c => c.String(maxLength: 60));
            AddColumn("sis.Address", "Region", c => c.String(maxLength: 20));
            AddColumn("sis.Address", "PostalCode", c => c.String(maxLength: 9));
            AddColumn("sis.Address", "County", c => c.String(maxLength: 20));
            DropColumn("sis.Address", "StreetAddress");
            DropColumn("sis.Address", "State");
            DropColumn("sis.Address", "ZipCode");
        }
        
        public override void Down()
        {
            AddColumn("sis.Address", "ZipCode", c => c.String(maxLength: 9));
            AddColumn("sis.Address", "State", c => c.String(maxLength: 2));
            AddColumn("sis.Address", "StreetAddress", c => c.String(maxLength: 60));
            DropColumn("sis.Address", "County");
            DropColumn("sis.Address", "PostalCode");
            DropColumn("sis.Address", "Region");
            DropColumn("sis.Address", "SecondAddressLine");
            DropColumn("sis.Address", "FirstAddressLine");
        }
    }
}
