namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "Lot_LotId", "dbo.Lots");
            DropIndex("dbo.Pictures", new[] { "Lot_LotId" });
            RenameColumn(table: "dbo.Pictures", name: "Lot_LotId", newName: "LotId");
            AlterColumn("dbo.Pictures", "LotId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pictures", "LotId");
            AddForeignKey("dbo.Pictures", "LotId", "dbo.Lots", "LotId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "LotId", "dbo.Lots");
            DropIndex("dbo.Pictures", new[] { "LotId" });
            AlterColumn("dbo.Pictures", "LotId", c => c.Int());
            RenameColumn(table: "dbo.Pictures", name: "LotId", newName: "Lot_LotId");
            CreateIndex("dbo.Pictures", "Lot_LotId");
            AddForeignKey("dbo.Pictures", "Lot_LotId", "dbo.Lots", "LotId");
        }
    }
}
