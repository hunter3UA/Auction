namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCarts : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lots", name: "ShopptingCart_ShoppingCartId", newName: "fk_CartId");
            RenameIndex(table: "dbo.Lots", name: "IX_ShopptingCart_ShoppingCartId", newName: "IX_fk_CartId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Lots", name: "IX_fk_CartId", newName: "IX_ShopptingCart_ShoppingCartId");
            RenameColumn(table: "dbo.Lots", name: "fk_CartId", newName: "ShopptingCart_ShoppingCartId");
        }
    }
}
