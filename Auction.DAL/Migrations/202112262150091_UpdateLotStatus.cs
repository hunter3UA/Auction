namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LotStatus",
                c => new
                    {
                        LotStatusId = c.Int(nullable: false, identity: true),
                        LotStatusName = c.String(),
                    })
                .PrimaryKey(t => t.LotStatusId);
            
            AddColumn("dbo.Lots", "fk_LotStatusId", c => c.Int());
            CreateIndex("dbo.Lots", "fk_LotStatusId");
            AddForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus", "LotStatusId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus");
            DropIndex("dbo.Lots", new[] { "fk_LotStatusId" });
            DropColumn("dbo.Lots", "fk_LotStatusId");
            DropTable("dbo.LotStatus");
        }
    }
}
