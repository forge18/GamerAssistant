namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLocationFromProposal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EventProposals", "Location");
            DropColumn("dbo.EventProposals", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventProposals", "Comment", c => c.String());
            AddColumn("dbo.EventProposals", "Location", c => c.String());
        }
    }
}
