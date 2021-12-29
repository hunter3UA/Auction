namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLot1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "CurrentPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lots", "CurrentPrice");
        }
    }
}
