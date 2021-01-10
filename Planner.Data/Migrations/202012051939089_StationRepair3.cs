namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StationRepair3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stations", "DepartureTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stations", "DepartureTime", c => c.DateTime());
        }
    }
}
