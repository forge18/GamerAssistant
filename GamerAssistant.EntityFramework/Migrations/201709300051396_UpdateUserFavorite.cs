namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserFavorite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserFavorites", "GameId", c => c.Int(nullable: false));
            DropColumn("dbo.UserFavorites", "TabletopGameId");
            DropColumn("dbo.UserFavorites", "VideoGameId");
            DropColumn("dbo.UserFavorites", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFavorites", "Comment", c => c.String());
            AddColumn("dbo.UserFavorites", "VideoGameId", c => c.Int(nullable: false));
            AddColumn("dbo.UserFavorites", "TabletopGameId", c => c.Int(nullable: false));
            DropColumn("dbo.UserFavorites", "GameId");
        }
    }
}
