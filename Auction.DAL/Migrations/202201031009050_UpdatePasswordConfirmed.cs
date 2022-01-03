namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePasswordConfirmed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logins", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logins", "IsConfirmed");
        }
    }
}
