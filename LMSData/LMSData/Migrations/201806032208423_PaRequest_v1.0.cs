namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequest_v10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaRequestAudits", "AutomobileRelated", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaRequestAudits", "RequestReassign", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaRequests", "AutomobileRelated", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaRequests", "RequestReassign", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaRequests", "RequestReassign");
            DropColumn("dbo.PaRequests", "AutomobileRelated");
            DropColumn("dbo.PaRequestAudits", "RequestReassign");
            DropColumn("dbo.PaRequestAudits", "AutomobileRelated");
        }
    }
}
