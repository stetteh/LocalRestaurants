namespace LocalEats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class types : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Type");
        }
    }
}
