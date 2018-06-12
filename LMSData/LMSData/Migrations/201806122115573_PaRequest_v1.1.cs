namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequest_v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaRequests", "NonMeds", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaRequests", "NonMeds");
        }
    }
}
