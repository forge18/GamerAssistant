namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserGameModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGames", "GameId", c => c.Int(nullable: false));
            DropColumn("dbo.UserGames", "TabletopGameId");
            DropColumn("dbo.UserGames", "VideoGameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserGames", "VideoGameId", c => c.Int(nullable: false));
            AddColumn("dbo.UserGames", "TabletopGameId", c => c.Int(nullable: false));
            DropColumn("dbo.UserGames", "GameId");
        }
    }
}
