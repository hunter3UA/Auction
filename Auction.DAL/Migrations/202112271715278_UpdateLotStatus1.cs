namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotStatus1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "fk_LotStatusId", c => c.Int());
            CreateIndex("dbo.Lots", "fk_LotStatusId");
            AddForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus", "LotStatusId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus");
            DropIndex("dbo.Lots", new[] { "fk_LotStatusId" });
            DropColumn("dbo.Lots", "fk_LotStatusId");
        }
    }
}
