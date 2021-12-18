namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
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
                        Description = c.String(nullable: false),
                        IsSoldOut = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        LotCode = c.Long(nullable: false),
                        SoldAt = c.DateTime(),
                        fk_SellerId = c.Int(nullable: false),
                        fk_CategoryId = c.Int(),
                        ShopptingCart_ShoppingCartId = c.Int(),
                    })
                .PrimaryKey(t => t.LotId)
                .ForeignKey("dbo.Categories", t => t.fk_CategoryId)
                .ForeignKey("dbo.ShopptingCarts", t => t.ShopptingCart_ShoppingCartId)
                .ForeignKey("dbo.Users", t => t.fk_SellerId, cascadeDelete: true)
                .Index(t => t.fk_SellerId)
                .Index(t => t.fk_CategoryId)
                .Index(t => t.ShopptingCart_ShoppingCartId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(nullable: false),
                        IsTittle = c.Boolean(nullable: false),
                        LotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Lots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(),
                        Balance = c.Double(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "fk_SellerId", "dbo.Users");
            DropForeignKey("dbo.ShopptingCarts", "ShoppingCartId", "dbo.Users");
            DropForeignKey("dbo.Lots", "ShopptingCart_ShoppingCartId", "dbo.ShopptingCarts");
            DropForeignKey("dbo.Users", "fk_LoginId", "dbo.Logins");
            DropForeignKey("dbo.Pictures", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Lots", "fk_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Logins", "fk_AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.ShopptingCarts", new[] { "ShoppingCartId" });
            DropIndex("dbo.Users", new[] { "fk_LoginId" });
            DropIndex("dbo.Pictures", new[] { "LotId" });
            DropIndex("dbo.Lots", new[] { "ShopptingCart_ShoppingCartId" });
            DropIndex("dbo.Lots", new[] { "fk_CategoryId" });
            DropIndex("dbo.Lots", new[] { "fk_SellerId" });
            DropIndex("dbo.Logins", new[] { "fk_AccountTypeId" });
            DropIndex("dbo.Logins", "UniqueEmail");
            DropIndex("dbo.Categories", "UniqueCategoryName");
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
