namespace LocalEats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
