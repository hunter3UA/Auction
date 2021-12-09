namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lots", "LotCode", c => c.Guid(nullable: false));
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false));
            DropColumn("dbo.Lots", "LotCode");
        }
    }
}
