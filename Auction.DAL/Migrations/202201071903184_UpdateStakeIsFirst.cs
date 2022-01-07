namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStakeIsFirst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stakes", "IsFirst", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stakes", "IsFirst");
        }
    }
}
