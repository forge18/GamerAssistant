namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMechanicsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TabletopGameMechanics", "MechanicName", c => c.String());
            DropColumn("dbo.TabletopGameMechanics", "MechnanicName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TabletopGameMechanics", "MechnanicName", c => c.String());
            DropColumn("dbo.TabletopGameMechanics", "MechanicName");
        }
    }
}
