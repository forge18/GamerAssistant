namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGameModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TabletopGames", "YearPublished", c => c.String());
            AlterColumn("dbo.TabletopGames", "MinPlayers", c => c.String());
            AlterColumn("dbo.TabletopGames", "MaxPlayers", c => c.String());
            AlterColumn("dbo.TabletopGames", "PlayTime", c => c.String());
            AlterColumn("dbo.TabletopGames", "Image", c => c.String());
            AlterColumn("dbo.TabletopGames", "ThumbnailImage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TabletopGames", "ThumbnailImage", c => c.Int(nullable: false));
            AlterColumn("dbo.TabletopGames", "Image", c => c.Int(nullable: false));
            AlterColumn("dbo.TabletopGames", "PlayTime", c => c.Int(nullable: false));
            AlterColumn("dbo.TabletopGames", "MaxPlayers", c => c.Int(nullable: false));
            AlterColumn("dbo.TabletopGames", "MinPlayers", c => c.Int(nullable: false));
            AlterColumn("dbo.TabletopGames", "YearPublished", c => c.Int(nullable: false));
        }
    }
}
