namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stationrepair2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stations", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Stations", "DeparturePlace", c => c.String(nullable: false, maxLength: 2048));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stations", "DeparturePlace", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Stations", "Name", c => c.String(nullable: false));
        }
    }
}
