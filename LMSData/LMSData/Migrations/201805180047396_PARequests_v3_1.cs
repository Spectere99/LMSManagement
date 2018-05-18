namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_v3_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InsuranceCompanies", "CompanyCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InsuranceCompanies", "CompanyCode");
        }
    }
}
