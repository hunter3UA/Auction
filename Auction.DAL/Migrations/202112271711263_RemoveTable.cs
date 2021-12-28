namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LotStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LotStatus",
                c => new
                    {
                        LotStatusId = c.Int(nullable: false, identity: true),
                        LotStatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LotStatusId);
            
        }
    }
}
