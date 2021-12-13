namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Lots", "LotCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "LotCode", c => c.Guid(nullable: false));
        }
    }
}
