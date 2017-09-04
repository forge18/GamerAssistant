namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.event_attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        attachment_url = c.String(),
                        updated_on = c.DateTime(nullable: false),
                        owner_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.event_date_options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        location = c.String(),
                        comment = c.String(),
                        owner_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.events_games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        event_id = c.Int(nullable: false),
                        game_id = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        location = c.String(),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.event_tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        event_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        title = c.String(),
                        description = c.String(),
                        comment = c.String(),
                        completed_on = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.event_users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        event_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        attending = c.Boolean(nullable: false),
                        comment = c.String(),
                        updated_on = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.game_categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
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
            
            CreateTable(
                "dbo.game_images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        image_url = c.String(),
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
                        owner_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.game_themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.user_favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        game_id = c.Int(nullable: false),
                        comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.user_votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        event_date_option_id = c.Int(nullable: false),
                        event_game_id = c.Int(nullable: false),
                        vote = c.Byte(nullable: false),
                        updated_on = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.user_votes");
            DropTable("dbo.user_favorites");
            DropTable("dbo.game_themes");
            DropTable("dbo.games");
            DropTable("dbo.game_play_dates");
            DropTable("dbo.game_mechanics");
            DropTable("dbo.game_images");
            DropTable("dbo.game_expansions");
            DropTable("dbo.game_categories");
            DropTable("dbo.event_users");
            DropTable("dbo.event_tasks");
            DropTable("dbo.events");
            DropTable("dbo.events_games");
            DropTable("dbo.event_date_options");
            DropTable("dbo.event_attachments");
        }
    }
}
