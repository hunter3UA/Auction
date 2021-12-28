namespace Auction.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNitfications : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Notifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        fk_LoginId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId);
            
        }
    }
}
