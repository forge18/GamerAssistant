namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorCodeAndRemoveUnneededDataPoints : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TabletopCategories", newName: "Categories");
            RenameTable(name: "dbo.TabletopGameCategories", newName: "GameCategories");
            RenameTable(name: "dbo.TabletopGameExpansions", newName: "GameExpansions");
            RenameTable(name: "dbo.TabletopGameMechanics", newName: "GameMechanics");
            RenameTable(name: "dbo.TabletopGames", newName: "Games");
            RenameTable(name: "dbo.VideoGameGenres", newName: "GameGenres");
            RenameTable(name: "dbo.VideoGamePlatforms", newName: "GamePlatforms");
            CreateTable(
                "dbo.Mechanics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "IsExpansion", c => c.Boolean(nullable: false));
            AddColumn("dbo.Games", "ParentGameId", c => c.Int(nullable: false));
            DropTable("dbo.TabletopMechanics");
            DropTable("dbo.VideoCategories");
            DropTable("dbo.VideoGameCategories");
            DropTable("dbo.VideoGames");
            DropTable("dbo.VideoGenres");
            DropTable("dbo.VideoPlatforms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VideoPlatforms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoSourceType = c.Int(nullable: false),
                        VideoSourceGameId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.Int(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoGameCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        VideoCategoryId = c.Int(nullable: false),
                        VideoCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TabletopMechanics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Games", "ParentGameId");
            DropColumn("dbo.Games", "IsExpansion");
            DropTable("dbo.Platforms");
            DropTable("dbo.Genres");
            DropTable("dbo.Mechanics");
            RenameTable(name: "dbo.GamePlatforms", newName: "VideoGamePlatforms");
            RenameTable(name: "dbo.GameGenres", newName: "VideoGameGenres");
            RenameTable(name: "dbo.Games", newName: "TabletopGames");
            RenameTable(name: "dbo.GameMechanics", newName: "TabletopGameMechanics");
            RenameTable(name: "dbo.GameExpansions", newName: "TabletopGameExpansions");
            RenameTable(name: "dbo.GameCategories", newName: "TabletopGameCategories");
            RenameTable(name: "dbo.Categories", newName: "TabletopCategories");
        }
    }
}
