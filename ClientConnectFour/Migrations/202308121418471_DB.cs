namespace ClientConnectFour.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameViews",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameViews");
        }
    }
}
