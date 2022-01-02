namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotsBuyerId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "fk_BuyerId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lots", "fk_BuyerId");
        }
    }
}
