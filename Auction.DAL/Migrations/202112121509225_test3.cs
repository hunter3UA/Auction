namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Lots", "SoldAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "SoldAt", c => c.DateTime(nullable: false));
        }
    }
}
