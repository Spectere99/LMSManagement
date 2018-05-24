namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_V4_4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaRequests", "InsuranceCompany_Id", "dbo.InsuranceCompanies");
            DropIndex("dbo.PaRequests", new[] { "InsuranceCompany_Id" });
            AlterColumn("dbo.PaRequests", "InsuranceCompany_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaRequests", "InsuranceCompany_Id", c => c.Int());
            CreateIndex("dbo.PaRequests", "InsuranceCompany_Id");
            AddForeignKey("dbo.PaRequests", "InsuranceCompany_Id", "dbo.InsuranceCompanies", "Id");
        }
    }
}
