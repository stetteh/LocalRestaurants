namespace LocalEats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetomenu : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Menus", "Name");
            DropColumn("dbo.Menus", "Description");
            DropColumn("dbo.MenuVms", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MenuVms", "Name", c => c.String());
            AddColumn("dbo.Menus", "Description", c => c.String());
            AddColumn("dbo.Menus", "Name", c => c.String());
        }
    }
}
