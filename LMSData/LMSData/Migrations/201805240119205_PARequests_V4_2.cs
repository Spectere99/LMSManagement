namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PARequests_V4_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lookups", "Type_Id", "dbo.LookupTypes");
            DropIndex("dbo.Lookups", new[] { "Type_Id" });
            RenameColumn(table: "dbo.Lookups", name: "Type_Id", newName: "LookupTypeId");
            AlterColumn("dbo.Lookups", "LookupTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lookups", "LookupTypeId");
            AddForeignKey("dbo.Lookups", "LookupTypeId", "dbo.LookupTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lookups", "LookupTypeId", "dbo.LookupTypes");
            DropIndex("dbo.Lookups", new[] { "LookupTypeId" });
            AlterColumn("dbo.Lookups", "LookupTypeId", c => c.Int());
            RenameColumn(table: "dbo.Lookups", name: "LookupTypeId", newName: "Type_Id");
            CreateIndex("dbo.Lookups", "Type_Id");
            AddForeignKey("dbo.Lookups", "Type_Id", "dbo.LookupTypes", "Id");
        }
    }
}
