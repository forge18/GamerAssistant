namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeParentGameIdName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "SourceParentGameId", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "ParentGameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "ParentGameId", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "SourceParentGameId");
        }
    }
}
