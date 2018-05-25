namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_V4_7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaRequestAudits", "ActionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaRequestAudits", "ActionDate");
        }
    }
}
