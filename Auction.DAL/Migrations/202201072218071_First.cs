namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        AccountTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.CategoryId)
                .Index(t => t.CategoryName, unique: true, name: "UniqueCategoryName");
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 200),
                        PasswordHash = c.Binary(nullable: false),
                        PasswordSalt = c.Binary(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        RegistredAt = c.DateTime(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        fk_AccountTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoginId)
                .ForeignKey("dbo.AccountTypes", t => t.fk_AccountTypeId, cascadeDelete: true)
                .Index(t => t.Email, unique: true, name: "UniqueEmail")
                .Index(t => t.fk_AccountTypeId);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        LotId = c.Int(nullable: false, identity: true),
                        LotName = c.String(nullable: false, maxLength: 500),
                        Price = c.Double(nullable: false),
                        CurrentPrice = c.Double(nullable: false),
                        Step = c.Double(nullable: false),
                        Description = c.String(nullable: false),
                        IsSoldOut = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        EndAt = c.DateTime(nullable: false),
                        LotCode = c.Long(nullable: false),
                        SoldAt = c.DateTime(),
                        fk_SellerId = c.Int(nullable: false),
                        fk_CategoryId = c.Int(),
                        fk_LotStatusId = c.Int(nullable: false),
                        fk_CartId = c.Int(),
                        fk_BuyerId = c.Int(),
                    })
                .PrimaryKey(t => t.LotId)
                .ForeignKey("dbo.Categories", t => t.fk_CategoryId)
                .ForeignKey("dbo.ShopptingCarts", t => t.fk_CartId)
                .ForeignKey("dbo.Users", t => t.fk_SellerId, cascadeDelete: true)
                .ForeignKey("dbo.LotStatus", t => t.fk_LotStatusId, cascadeDelete: true)
                .Index(t => t.fk_SellerId)
                .Index(t => t.fk_CategoryId)
                .Index(t => t.fk_LotStatusId)
                .Index(t => t.fk_CartId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(nullable: false),
                        IsTittle = c.Boolean(nullable: false),
                        LotId = c.Int(),
                        fk_NewsId = c.Long(),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .ForeignKey("dbo.News", t => t.fk_NewsId)
                .Index(t => t.LotId)
                .Index(t => t.fk_NewsId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(),
                        RegistredAt = c.DateTime(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        fk_LoginId = c.Int(nullable: false),
                        fk_ShoppingCartId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Logins", t => t.fk_LoginId, cascadeDelete: true)
                .Index(t => t.fk_LoginId);
            
            CreateTable(
                "dbo.ShopptingCarts",
                c => new
                    {
                        ShoppingCartId = c.Int(nullable: false),
                        fk_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ShoppingCartId)
                .ForeignKey("dbo.Users", t => t.ShoppingCartId)
                .Index(t => t.ShoppingCartId);
            
            CreateTable(
                "dbo.Stakes",
                c => new
                    {
                        StakeId = c.Long(nullable: false, identity: true),
                        fk_LotId = c.Int(nullable: false),
                        fk_UserId = c.Int(nullable: false),
                        Sum = c.Double(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StakeId)
                .ForeignKey("dbo.Lots", t => t.fk_LotId, cascadeDelete: true)
                .Index(t => t.fk_LotId);
            
            CreateTable(
                "dbo.LotStatus",
                c => new
                    {
                        LotStatusId = c.Int(nullable: false, identity: true),
                        LotStatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LotStatusId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Long(nullable: false, identity: true),
                        NewsTittle = c.String(nullable: false),
                        NewsText = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "fk_NewsId", "dbo.News");
            DropForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus");
            DropForeignKey("dbo.Stakes", "fk_LotId", "dbo.Lots");
            DropForeignKey("dbo.Lots", "fk_SellerId", "dbo.Users");
            DropForeignKey("dbo.ShopptingCarts", "ShoppingCartId", "dbo.Users");
            DropForeignKey("dbo.Lots", "fk_CartId", "dbo.ShopptingCarts");
            DropForeignKey("dbo.Users", "fk_LoginId", "dbo.Logins");
            DropForeignKey("dbo.Pictures", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Lots", "fk_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Logins", "fk_AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.Stakes", new[] { "fk_LotId" });
            DropIndex("dbo.ShopptingCarts", new[] { "ShoppingCartId" });
            DropIndex("dbo.Users", new[] { "fk_LoginId" });
            DropIndex("dbo.Pictures", new[] { "fk_NewsId" });
            DropIndex("dbo.Pictures", new[] { "LotId" });
            DropIndex("dbo.Lots", new[] { "fk_CartId" });
            DropIndex("dbo.Lots", new[] { "fk_LotStatusId" });
            DropIndex("dbo.Lots", new[] { "fk_CategoryId" });
            DropIndex("dbo.Lots", new[] { "fk_SellerId" });
            DropIndex("dbo.Logins", new[] { "fk_AccountTypeId" });
            DropIndex("dbo.Logins", "UniqueEmail");
            DropIndex("dbo.Categories", "UniqueCategoryName");
            DropTable("dbo.News");
            DropTable("dbo.LotStatus");
            DropTable("dbo.Stakes");
            DropTable("dbo.ShopptingCarts");
            DropTable("dbo.Users");
            DropTable("dbo.Pictures");
            DropTable("dbo.Lots");
            DropTable("dbo.Logins");
            DropTable("dbo.Categories");
            DropTable("dbo.AccountTypes");
        }
    }
}
