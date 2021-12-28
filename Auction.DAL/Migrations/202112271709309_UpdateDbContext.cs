namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus");
            DropIndex("dbo.Lots", new[] { "fk_LotStatusId" });
            DropColumn("dbo.Lots", "fk_LotStatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "fk_LotStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lots", "fk_LotStatusId");
            AddForeignKey("dbo.Lots", "fk_LotStatusId", "dbo.LotStatus", "LotStatusId", cascadeDelete: true);
        }
    }
}
