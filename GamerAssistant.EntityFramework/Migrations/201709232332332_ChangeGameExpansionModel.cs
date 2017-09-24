namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGameExpansionModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TabletopGameExpansions", "Name");
            DropColumn("dbo.TabletopGameExpansions", "YearPublished");
            DropColumn("dbo.TabletopGameExpansions", "MinPlayer");
            DropColumn("dbo.TabletopGameExpansions", "MaxPlayer");
            DropColumn("dbo.TabletopGameExpansions", "ThumbnailImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TabletopGameExpansions", "ThumbnailImage", c => c.String());
            AddColumn("dbo.TabletopGameExpansions", "MaxPlayer", c => c.Int(nullable: false));
            AddColumn("dbo.TabletopGameExpansions", "MinPlayer", c => c.Int(nullable: false));
            AddColumn("dbo.TabletopGameExpansions", "YearPublished", c => c.Int(nullable: false));
            AddColumn("dbo.TabletopGameExpansions", "Name", c => c.String());
        }
    }
}
