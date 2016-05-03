namespace LocalEats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorchanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuVms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MenuVms");
        }
    }
}
