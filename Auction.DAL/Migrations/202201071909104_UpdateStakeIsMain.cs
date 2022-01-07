namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStakeIsMain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stakes", "IsMain", c => c.Boolean(nullable: false));
            DropColumn("dbo.Stakes", "IsFirst");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stakes", "IsFirst", c => c.Boolean(nullable: false));
            DropColumn("dbo.Stakes", "IsMain");
        }
    }
}
