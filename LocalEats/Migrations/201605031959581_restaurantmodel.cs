namespace LocalEats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restaurantmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        FoodImage = c.String(),
                        Price = c.Double(nullable: false),
                        Type = c.Int(nullable: false),
                        ParentMenu_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.ParentMenu_Id)
                .Index(t => t.ParentMenu_Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.Int(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Description = c.String(),
                        Features = c.String(),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Score = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Restaurant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Photos", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Menus", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Drinks", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Foods", "ParentMenu_Id", "dbo.Menus");
            DropIndex("dbo.Reviews", new[] { "Restaurant_Id" });
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Photos", new[] { "Restaurant_Id" });
            DropIndex("dbo.Drinks", new[] { "Restaurant_Id" });
            DropIndex("dbo.Foods", new[] { "ParentMenu_Id" });
            DropIndex("dbo.Menus", new[] { "Restaurant_Id" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Photos");
            DropTable("dbo.Drinks");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Foods");
            DropTable("dbo.Menus");
        }
    }
}
