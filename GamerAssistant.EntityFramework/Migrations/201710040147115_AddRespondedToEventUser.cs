namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRespondedToEventUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventUsers", "Responded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventUsers", "Responded");
        }
    }
}
