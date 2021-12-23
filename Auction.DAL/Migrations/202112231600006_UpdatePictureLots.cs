namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePictureLots : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "LotId", "dbo.Lots");
            DropIndex("dbo.Pictures", new[] { "LotId" });
            AlterColumn("dbo.Pictures", "LotId", c => c.Int());
            CreateIndex("dbo.Pictures", "LotId");
            AddForeignKey("dbo.Pictures", "LotId", "dbo.Lots", "LotId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "LotId", "dbo.Lots");
            DropIndex("dbo.Pictures", new[] { "LotId" });
            AlterColumn("dbo.Pictures", "LotId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pictures", "LotId");
            AddForeignKey("dbo.Pictures", "LotId", "dbo.Lots", "LotId", cascadeDelete: true);
        }
    }
}
