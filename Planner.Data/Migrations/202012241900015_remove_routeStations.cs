namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_routeStations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteStations", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.RouteStations", "StationId", "dbo.Stations");
            DropIndex("dbo.RouteStations", new[] { "RouteId" });
            DropIndex("dbo.RouteStations", new[] { "StationId" });
            DropTable("dbo.RouteStations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        RouteId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RouteId, t.StationId });
            
            CreateIndex("dbo.RouteStations", "StationId");
            CreateIndex("dbo.RouteStations", "RouteId");
            AddForeignKey("dbo.RouteStations", "StationId", "dbo.Stations", "StationId", cascadeDelete: true);
            AddForeignKey("dbo.RouteStations", "RouteId", "dbo.Routes", "RouteId", cascadeDelete: true);
        }
    }
}
