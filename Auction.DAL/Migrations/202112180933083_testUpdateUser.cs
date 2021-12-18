namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testUpdateUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsEnabled");
        }
    }
}
