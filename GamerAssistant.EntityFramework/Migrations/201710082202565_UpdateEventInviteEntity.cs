namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEventInviteEntity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EventUsers", newName: "EventInvites");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.EventInvites", newName: "EventUsers");
        }
    }
}
