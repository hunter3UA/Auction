namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "LotCode", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lots", "LotCode");
        }
    }
}
