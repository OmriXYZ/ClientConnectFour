namespace ClientConnectFour.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameViews", "Moves", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameViews", "Moves");
        }
    }
}
