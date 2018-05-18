namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lookups", "Archived", c => c.Boolean(nullable: false));
            AddColumn("dbo.LookupTypes", "Archived", c => c.Boolean(nullable: false));
            AddColumn("dbo.InsuranceCompanies", "Archived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Archived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Archived");
            DropColumn("dbo.InsuranceCompanies", "Archived");
            DropColumn("dbo.LookupTypes", "Archived");
            DropColumn("dbo.Lookups", "Archived");
        }
    }
}
