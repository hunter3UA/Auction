namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewsTable : DbMigration
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
            
            AddColumn("dbo.Pictures", "fk_NewsId", c => c.Long());
            CreateIndex("dbo.Pictures", "fk_NewsId");
            AddForeignKey("dbo.Pictures", "fk_NewsId", "dbo.News", "NewsId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "fk_NewsId", "dbo.News");
            DropIndex("dbo.Pictures", new[] { "fk_NewsId" });
            DropColumn("dbo.Pictures", "fk_NewsId");
            DropTable("dbo.News");
        }
    }
}
