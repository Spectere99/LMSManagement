namespace LIMSData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaRequest_v15 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FileUploadLogs", name: "Module_Id", newName: "ModuleId");
            RenameIndex(table: "dbo.FileUploadLogs", name: "IX_Module_Id", newName: "IX_ModuleId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FileUploadLogs", name: "IX_ModuleId", newName: "IX_Module_Id");
            RenameColumn(table: "dbo.FileUploadLogs", name: "ModuleId", newName: "Module_Id");
        }
    }
}
