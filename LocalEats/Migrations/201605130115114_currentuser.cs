namespace LocalEats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currentuser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reviews", name: "User_Id", newName: "CurrentUser_Id");
            RenameIndex(table: "dbo.Reviews", name: "IX_User_Id", newName: "IX_CurrentUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Reviews", name: "IX_CurrentUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Reviews", name: "CurrentUser_Id", newName: "User_Id");
        }
    }
}
