namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotStatuses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LotStatus", "LotStatusName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LotStatus", "LotStatusName", c => c.String());
        }
    }
}
