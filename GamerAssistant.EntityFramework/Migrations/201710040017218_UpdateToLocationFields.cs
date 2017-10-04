namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToLocationFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "LocationNickname", c => c.String());
            AddColumn("dbo.Events", "Address", c => c.String());
            AddColumn("dbo.Events", "City", c => c.String());
            AddColumn("dbo.Events", "State", c => c.String());
            AddColumn("dbo.Events", "Zip", c => c.String());
            DropColumn("dbo.Events", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Location", c => c.String());
            DropColumn("dbo.Events", "Zip");
            DropColumn("dbo.Events", "State");
            DropColumn("dbo.Events", "City");
            DropColumn("dbo.Events", "Address");
            DropColumn("dbo.Events", "LocationNickname");
        }
    }
}
