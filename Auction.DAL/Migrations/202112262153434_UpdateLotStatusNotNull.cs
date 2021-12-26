namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotStatusNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus");
            DropIndex("dbo.Lots", new[] { "fk_LotStatusId" });
            AlterColumn("dbo.Lots", "fk_LotStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lots", "fk_LotStatusId");
            AddForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus", "LotStatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus");
            DropIndex("dbo.Lots", new[] { "fk_LotStatusId" });
            AlterColumn("dbo.Lots", "fk_LotStatusId", c => c.Int());
            CreateIndex("dbo.Lots", "fk_LotStatusId");
            AddForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus", "LotStatusId");
        }
    }
}
