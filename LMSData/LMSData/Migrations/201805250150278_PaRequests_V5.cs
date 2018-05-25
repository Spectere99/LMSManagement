namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequests_V5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaRequestAudits", "Priority", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaRequestAudits", "Completed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaRequestAudits", "CompletedTimeStamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaRequestAudits", "BillingStatus", c => c.Int(nullable: false));
            AddColumn("dbo.PaRequests", "Priority", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaRequests", "Completed", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaRequests", "CompletedTimeStamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaRequests", "BillingStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaRequests", "BillingStatus");
            DropColumn("dbo.PaRequests", "CompletedTimeStamp");
            DropColumn("dbo.PaRequests", "Completed");
            DropColumn("dbo.PaRequests", "Priority");
            DropColumn("dbo.PaRequestAudits", "BillingStatus");
            DropColumn("dbo.PaRequestAudits", "CompletedTimeStamp");
            DropColumn("dbo.PaRequestAudits", "Completed");
            DropColumn("dbo.PaRequestAudits", "Priority");
        }
    }
}
