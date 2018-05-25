namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_V4_5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaRequests", "Status_Id", "dbo.Lookups");
            DropIndex("dbo.PaRequests", new[] { "Status_Id" });
            AddColumn("dbo.PaRequests", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.PaRequests", "Status_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaRequests", "Status_Id", c => c.Int());
            DropColumn("dbo.PaRequests", "Status");
            CreateIndex("dbo.PaRequests", "Status_Id");
            AddForeignKey("dbo.PaRequests", "Status_Id", "dbo.Lookups", "Id");
        }
    }
}
