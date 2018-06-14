namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequest_v12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaRequestAudits", "NonMeds", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaRequestAudits", "NonMeds");
        }
    }
}
