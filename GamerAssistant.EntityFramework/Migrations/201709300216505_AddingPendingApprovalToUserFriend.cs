namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPendingApprovalToUserFriend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserFriends", "PendingApproval", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserFriends", "PendingApproval");
        }
    }
}
