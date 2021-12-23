namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Long(nullable: false, identity: true),
                        NewsTittle = c.String(nullable: false),
                        NewsText = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId);
            
            AddColumn("dbo.Pictures", "News_NewsId", c => c.Long());
            CreateIndex("dbo.Pictures", "News_NewsId");
            AddForeignKey("dbo.Pictures", "News_NewsId", "dbo.News", "NewsId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "News_NewsId", "dbo.News");
            DropIndex("dbo.Pictures", new[] { "News_NewsId" });
            DropColumn("dbo.Pictures", "News_NewsId");
            DropTable("dbo.News");
        }
    }
}
