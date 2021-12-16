namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(nullable: false),
                        IsTittle = c.Boolean(nullable: false),
                        Lot_LotId = c.Int(),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Lots", t => t.Lot_LotId)
                .Index(t => t.Lot_LotId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pictures", "Lot_LotId", "dbo.Lots");
            DropIndex("dbo.Pictures", new[] { "Lot_LotId" });
            DropTable("dbo.Pictures");
        }
    }
}
