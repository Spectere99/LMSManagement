namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequest_v13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaRequestAudits", "AssignedToId", c => c.Int(nullable: false));
            AddColumn("dbo.PaRequests", "AssignedToId", c => c.Int(nullable: false));
            CreateIndex("dbo.PaRequests", "AssignedToId");
            AddForeignKey("dbo.PaRequests", "AssignedToId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.PaRequestAudits", "AssignedTo");
            DropColumn("dbo.PaRequests", "AssignedTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaRequests", "AssignedTo", c => c.String());
            AddColumn("dbo.PaRequestAudits", "AssignedTo", c => c.String());
            DropForeignKey("dbo.PaRequests", "AssignedToId", "dbo.Users");
            DropIndex("dbo.PaRequests", new[] { "AssignedToId" });
            DropColumn("dbo.PaRequests", "AssignedToId");
            DropColumn("dbo.PaRequestAudits", "AssignedToId");
        }
    }
}
