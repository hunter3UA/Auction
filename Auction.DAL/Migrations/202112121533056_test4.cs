namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "SoldAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lots", "SoldAt");
        }
    }
}
