namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsUpdating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "CreatedAt");
        }
    }
}
