namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_v3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaRequests", "Approval", c => c.DateTime());
            AlterColumn("dbo.PaRequests", "Denial", c => c.DateTime());
            AlterColumn("dbo.PaRequests", "Assigned", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaRequests", "Assigned", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PaRequests", "Denial", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PaRequests", "Approval", c => c.DateTime(nullable: false));
        }
    }
}
