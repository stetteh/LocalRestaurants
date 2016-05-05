namespace LocalEats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class additionalchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drinks", "Description", c => c.String());
            AddColumn("dbo.MenuVms", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuVms", "Type");
            DropColumn("dbo.Drinks", "Description");
        }
    }
}
