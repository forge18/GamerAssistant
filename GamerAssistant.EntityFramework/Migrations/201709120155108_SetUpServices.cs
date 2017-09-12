namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetUpServices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.user_votes", "due_date", c => c.DateTime(nullable: false));
            DropColumn("dbo.games", "owner_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.games", "owner_id", c => c.Int(nullable: false));
            DropColumn("dbo.user_votes", "due_date");
        }
    }
}
