namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequest_v14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaRequests", "AssignedToId", "dbo.Users");
            DropIndex("dbo.PaRequests", new[] { "AssignedToId" });
            AlterColumn("dbo.PaRequests", "AssignedToId", c => c.Int());
            CreateIndex("dbo.PaRequests", "AssignedToId");
            AddForeignKey("dbo.PaRequests", "AssignedToId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaRequests", "AssignedToId", "dbo.Users");
            DropIndex("dbo.PaRequests", new[] { "AssignedToId" });
            AlterColumn("dbo.PaRequests", "AssignedToId", c => c.Int(nullable: false));
            CreateIndex("dbo.PaRequests", "AssignedToId");
            AddForeignKey("dbo.PaRequests", "AssignedToId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
