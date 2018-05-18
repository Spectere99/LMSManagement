using System.Data.Entity.Migrations;

namespace LIMSData.Migrations
{
    public partial class LookupModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LookupValue = c.String(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LookupTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.LookupTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lookups", "Type_Id", "dbo.LookupTypes");
            DropIndex("dbo.Lookups", new[] { "Type_Id" });
            DropTable("dbo.LookupTypes");
            DropTable("dbo.Lookups");
        }
    }
}
