namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewsPictures : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pictures", "fk_NewsId", "dbo.News");
            DropIndex("dbo.Pictures", new[] { "fk_NewsId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Pictures", "fk_NewsId");
            AddForeignKey("dbo.Pictures", "fk_NewsId", "dbo.News", "NewsId");
        }
    }
}
