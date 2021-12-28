namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbTables : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.LotStatus");
        }
    }
}
