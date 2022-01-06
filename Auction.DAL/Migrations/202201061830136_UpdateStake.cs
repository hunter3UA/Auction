namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStake : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stakes", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stakes", "IsRemoved");
        }
    }
}
