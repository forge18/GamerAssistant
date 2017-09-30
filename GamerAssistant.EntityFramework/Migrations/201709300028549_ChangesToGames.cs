namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToGames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameCategories", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.GameCategories", "CategoryName", c => c.String());
            AddColumn("dbo.GameGenres", "GenreId", c => c.Int(nullable: false));
            AddColumn("dbo.GameGenres", "GenreName", c => c.String());
            AddColumn("dbo.GamePlatforms", "PlatformId", c => c.Int(nullable: false));
            AddColumn("dbo.GamePlatforms", "PlatformName", c => c.String());
            DropColumn("dbo.GameCategories", "TabletopCategoryId");
            DropColumn("dbo.GameCategories", "TabletopCategoryName");
            DropColumn("dbo.GameGenres", "VideoGenreId");
            DropColumn("dbo.GameGenres", "VideoGenreName");
            DropColumn("dbo.GamePlatforms", "VideoPlatformId");
            DropColumn("dbo.GamePlatforms", "VideoPlatformName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GamePlatforms", "VideoPlatformName", c => c.String());
            AddColumn("dbo.GamePlatforms", "VideoPlatformId", c => c.Int(nullable: false));
            AddColumn("dbo.GameGenres", "VideoGenreName", c => c.String());
            AddColumn("dbo.GameGenres", "VideoGenreId", c => c.Int(nullable: false));
            AddColumn("dbo.GameCategories", "TabletopCategoryName", c => c.String());
            AddColumn("dbo.GameCategories", "TabletopCategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.GamePlatforms", "PlatformName");
            DropColumn("dbo.GamePlatforms", "PlatformId");
            DropColumn("dbo.GameGenres", "GenreName");
            DropColumn("dbo.GameGenres", "GenreId");
            DropColumn("dbo.GameCategories", "CategoryName");
            DropColumn("dbo.GameCategories", "CategoryId");
        }
    }
}
