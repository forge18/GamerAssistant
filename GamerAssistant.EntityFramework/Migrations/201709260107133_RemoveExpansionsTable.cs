namespace GamerAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExpansionsTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.GameExpansions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GameExpansions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        ExpansionGameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
