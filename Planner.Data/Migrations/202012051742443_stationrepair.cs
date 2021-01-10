namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stationrepair : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "Name");
        }
    }
}
