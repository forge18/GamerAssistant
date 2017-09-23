namespace GamerAssistant.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ReconfigureSqlDatabase : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.events", newName: "Events");
            RenameTable(name: "dbo.event_attachments", newName: "EventAttachments");
            RenameTable(name: "dbo.event_date_options", newName: "EventProposals");
            RenameTable(name: "dbo.events_games", newName: "EventGames");
            RenameTable(name: "dbo.event_tasks", newName: "EventTasks");
            RenameTable(name: "dbo.event_users", newName: "EventUsers");
            RenameTable(name: "dbo.game_categories", newName: "TabletopCategories");
            RenameTable(name: "dbo.user_favorites", newName: "UserFavorites");
            RenameTable(name: "dbo.user_votes", newName: "UserVotes");
            RenameColumn(table: "dbo.EventAttachments", name: "attachment_url", newName: "AttachmentUrl");
            RenameColumn(table: "dbo.EventAttachments", name: "description", newName: "Description");
            RenameColumn(table: "dbo.EventAttachments", name: "updated_on", newName: "UpdatedOn");
            RenameColumn(table: "dbo.EventAttachments", name: "owner_id", newName: "OwnerId");
            RenameColumn(table: "dbo.EventProposals", name: "start_date", newName: "StartDateTime");
            RenameColumn(table: "dbo.EventProposals", name: "end_date", newName: "EndDateTime");
            RenameColumn(table: "dbo.EventProposals", name: "owner_id", newName: "OwnerId");
            RenameColumn(table: "dbo.EventProposals", name: "location", newName: "Location");
            RenameColumn(table: "dbo.EventProposals", name: "comment", newName: "Comment");
            RenameColumn(table: "dbo.EventGames", name: "event_id", newName: "EventId");
            RenameColumn(table: "dbo.EventGames", name: "game_id", newName: "GameId");
            RenameColumn(table: "dbo.EventGames", name: "status", newName: "Status");
            RenameColumn(table: "dbo.Events", name: "start_date", newName: "StartDateTime");
            RenameColumn(table: "dbo.Events", name: "end_date", newName: "EndDateTime");
            RenameColumn(table: "dbo.Events", name: "title", newName: "Title");
            RenameColumn(table: "dbo.Events", name: "description", newName: "Description");
            RenameColumn(table: "dbo.Events", name: "location", newName: "Location");
            RenameColumn(table: "dbo.Events", name: "status", newName: "Status");
            RenameColumn(table: "dbo.EventTasks", name: "event_id", newName: "EventId");
            RenameColumn(table: "dbo.EventTasks", name: "user_id", newName: "UserId");
            RenameColumn(table: "dbo.EventTasks", name: "completed_on", newName: "CompletedOn");
            RenameColumn(table: "dbo.EventUsers", name: "event_id", newName: "EventId");
            RenameColumn(table: "dbo.EventUsers", name: "user_id", newName: "UserId");
            RenameColumn(table: "dbo.EventUsers", name: "updated_on", newName: "UpdatedOn");
            RenameColumn(table: "dbo.TabletopCategories", name: "name", newName: "Name");
            RenameColumn(table: "dbo.UserFavorites", name: "user_id", newName: "UserId");
            RenameColumn(table: "dbo.UserFavorites", name: "comment", newName: "Comment");
            RenameColumn(table: "dbo.UserVotes", name: "user_id", newName: "UserId");
            RenameColumn(table: "dbo.UserVotes", name: "event_date_option_id", newName: "EventDateOptionId");
            RenameColumn(table: "dbo.UserVotes", name: "event_game_id", newName: "EventGameId");
            RenameColumn(table: "dbo.UserVotes", name: "due_date", newName: "DueDate");
            RenameColumn(table: "dbo.UserVotes", name: "updated_on", newName: "UpdatedOn");
            RenameColumn(table: "dbo.UserVotes", name: "vote", newName: "Vote");
            CreateTable(
                "dbo.TabletopGameCategories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GameId = c.Int(nullable: false),
                    TabletopCategoryId = c.Int(nullable: false),
                    TabletopCategoryName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TabletopGameExpansions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GameId = c.Int(nullable: false),
                    ExpansionGameId = c.Int(nullable: false),
                    Name = c.String(),
                    YearPublished = c.Int(nullable: false),
                    MinPlayer = c.Int(nullable: false),
                    MaxPlayer = c.Int(nullable: false),
                    ThumbnailImage = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TabletopGameMechanics",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GameId = c.Int(nullable: false),
                    MechanicId = c.Int(nullable: false),
                    MechnanicName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TabletopGames",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TabletopSourceType = c.Int(nullable: false),
                    TabletopSourceGameId = c.Int(nullable: false),
                    GameType = c.String(),
                    Name = c.String(),
                    Description = c.String(),
                    YearPublished = c.Int(nullable: false),
                    MinPlayers = c.Int(nullable: false),
                    MaxPlayers = c.Int(nullable: false),
                    PlayTime = c.Int(nullable: false),
                    Image = c.Int(nullable: false),
                    ThumbnailImage = c.Int(nullable: false),
                    AddedOn = c.DateTime(nullable: false),
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

            CreateTable(
                "dbo.UserFriends",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    FriendUserId = c.Int(nullable: false),
                    AddedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserGames",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    TabletopGameId = c.Int(nullable: false),
                    VideoGameId = c.Int(nullable: false),
                    AddedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserSources",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    SourceType = c.Int(nullable: false),
                    SourceUserName = c.String(),
                    SourceAuth = c.String(),
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
                "dbo.VideoGameGenres",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GameId = c.Int(nullable: false),
                    VideoGenreId = c.Int(nullable: false),
                    VideoGenreName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VideoGamePlatforms",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GameId = c.Int(nullable: false),
                    VideoPlatformId = c.Int(nullable: false),
                    VideoPlatformName = c.String(),
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
                "dbo.VideoGenres",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VideoPlatforms",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.EventTasks", "Name", c => c.String());
            AddColumn("dbo.EventTasks", "Instructions", c => c.String());
            AddColumn("dbo.EventTasks", "CompletionComments", c => c.String());
            AddColumn("dbo.EventUsers", "Accepted", c => c.Boolean(nullable: false));
            AddColumn("dbo.EventUsers", "Attended", c => c.Boolean(nullable: false));
            AddColumn("dbo.EventUsers", "DeclineComment", c => c.String());
            AddColumn("dbo.UserFavorites", "TabletopGameId", c => c.Int(nullable: false));
            AddColumn("dbo.UserFavorites", "VideoGameId", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "StartDateTime", c => c.DateTime());
            AlterColumn("dbo.Events", "EndDateTime", c => c.DateTime());
            DropColumn("dbo.EventTasks", "title");
            DropColumn("dbo.EventTasks", "description");
            DropColumn("dbo.EventTasks", "comment");
            DropColumn("dbo.EventUsers", "attending");
            DropColumn("dbo.EventUsers", "comment");
            DropColumn("dbo.UserFavorites", "game_id");
            DropTable("dbo.game_expansions");
            DropTable("dbo.game_images");
            DropTable("dbo.game_mechanics");
            DropTable("dbo.game_play_dates");
            DropTable("dbo.games");
            DropTable("dbo.game_themes");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.game_themes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.games",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    name = c.String(),
                    description = c.String(),
                    min_players = c.Int(nullable: false),
                    max_players = c.Int(nullable: false),
                    game_type = c.Int(nullable: false),
                    category_id = c.Int(nullable: false),
                    theme_id = c.Int(nullable: false),
                    primary_mechanic_id = c.Int(nullable: false),
                    secondary_mechanic_id = c.Int(nullable: false),
                    primary_image_url = c.String(),
                    publisher_url = c.String(),
                    added_on = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.game_play_dates",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    game_id = c.Int(nullable: false),
                    play_date = c.DateTime(nullable: false),
                    number_of_players = c.Int(nullable: false),
                    rating = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.game_mechanics",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.game_images",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    image_url = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.game_expansions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    game_id = c.Int(nullable: false),
                    name = c.String(),
                    added_on = c.DateTime(nullable: false),
                    owner_id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.UserFavorites", "game_id", c => c.Int(nullable: false));
            AddColumn("dbo.EventUsers", "comment", c => c.String());
            AddColumn("dbo.EventUsers", "attending", c => c.Boolean(nullable: false));
            AddColumn("dbo.EventTasks", "comment", c => c.String());
            AddColumn("dbo.EventTasks", "description", c => c.String());
            AddColumn("dbo.EventTasks", "title", c => c.String());
            AlterColumn("dbo.Events", "EndDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "StartDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserFavorites", "VideoGameId");
            DropColumn("dbo.UserFavorites", "TabletopGameId");
            DropColumn("dbo.EventUsers", "DeclineComment");
            DropColumn("dbo.EventUsers", "Attended");
            DropColumn("dbo.EventUsers", "Accepted");
            DropColumn("dbo.EventTasks", "CompletionComments");
            DropColumn("dbo.EventTasks", "Instructions");
            DropColumn("dbo.EventTasks", "Name");
            DropTable("dbo.VideoPlatforms");
            DropTable("dbo.VideoGenres");
            DropTable("dbo.VideoGames");
            DropTable("dbo.VideoGamePlatforms");
            DropTable("dbo.VideoGameGenres");
            DropTable("dbo.VideoGameCategories");
            DropTable("dbo.VideoCategories");
            DropTable("dbo.UserSources");
            DropTable("dbo.UserGames");
            DropTable("dbo.UserFriends");
            DropTable("dbo.TabletopMechanics");
            DropTable("dbo.TabletopGames");
            DropTable("dbo.TabletopGameMechanics");
            DropTable("dbo.TabletopGameExpansions");
            DropTable("dbo.TabletopGameCategories");
            RenameColumn(table: "dbo.UserVotes", name: "UpdatedOn", newName: "updated_on");
            RenameColumn(table: "dbo.UserVotes", name: "DueDate", newName: "due_date");
            RenameColumn(table: "dbo.UserVotes", name: "EventGameId", newName: "event_game_id");
            RenameColumn(table: "dbo.UserVotes", name: "EventDateOptionId", newName: "event_date_option_id");
            RenameColumn(table: "dbo.UserVotes", name: "UserId", newName: "user_id");
            RenameColumn(table: "dbo.UserFavorites", name: "UserId", newName: "user_id");
            RenameColumn(table: "dbo.EventUsers", name: "UpdatedOn", newName: "updated_on");
            RenameColumn(table: "dbo.EventUsers", name: "UserId", newName: "user_id");
            RenameColumn(table: "dbo.EventUsers", name: "EventId", newName: "event_id");
            RenameColumn(table: "dbo.EventTasks", name: "CompletedOn", newName: "completed_on");
            RenameColumn(table: "dbo.EventTasks", name: "UserId", newName: "user_id");
            RenameColumn(table: "dbo.EventTasks", name: "EventId", newName: "event_id");
            RenameColumn(table: "dbo.Events", name: "EndDateTime", newName: "end_date");
            RenameColumn(table: "dbo.Events", name: "StartDateTime", newName: "start_date");
            RenameColumn(table: "dbo.EventGames", name: "GameId", newName: "game_id");
            RenameColumn(table: "dbo.EventGames", name: "EventId", newName: "event_id");
            RenameColumn(table: "dbo.EventProposals", name: "OwnerId", newName: "owner_id");
            RenameColumn(table: "dbo.EventProposals", name: "EndDateTime", newName: "end_date");
            RenameColumn(table: "dbo.EventProposals", name: "StartDateTime", newName: "start_date");
            RenameColumn(table: "dbo.EventAttachments", name: "OwnerId", newName: "owner_id");
            RenameColumn(table: "dbo.EventAttachments", name: "UpdatedOn", newName: "updated_on");
            RenameColumn(table: "dbo.EventAttachments", name: "AttachmentUrl", newName: "attachment_url");
            RenameTable(name: "dbo.UserVotes", newName: "user_votes");
            RenameTable(name: "dbo.UserFavorites", newName: "user_favorites");
            RenameTable(name: "dbo.TabletopCategories", newName: "game_categories");
            RenameTable(name: "dbo.EventUsers", newName: "event_users");
            RenameTable(name: "dbo.EventTasks", newName: "event_tasks");
            RenameTable(name: "dbo.EventGames", newName: "events_games");
            RenameTable(name: "dbo.EventProposals", newName: "event_date_options");
            RenameTable(name: "dbo.EventAttachments", newName: "event_attachments");
        }
    }
}
