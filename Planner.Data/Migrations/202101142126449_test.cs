 namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exports", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Regions", "StateId", "dbo.States");
            DropForeignKey("dbo.Passengers", "BoardingStationId", "dbo.Stations");
            DropForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.Passengers", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            AddForeignKey("dbo.Exports", "RouteId", "dbo.Routes", "RouteId");
            AddForeignKey("dbo.Routes", "ProviderId", "dbo.Providers", "ProviderId");
            AddForeignKey("dbo.Regions", "StateId", "dbo.States", "StateId");
            AddForeignKey("dbo.Passengers", "BoardingStationId", "dbo.Stations", "StationId");
            AddForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners", "OwnerId");
            AddForeignKey("dbo.Passengers", "RouteId", "dbo.Routes", "RouteId");
            AddForeignKey("dbo.Stations", "RegionId", "dbo.Regions", "RegionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Passengers", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.Passengers", "BoardingStationId", "dbo.Stations");
            DropForeignKey("dbo.Regions", "StateId", "dbo.States");
            DropForeignKey("dbo.Routes", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Exports", "RouteId", "dbo.Routes");
            AddForeignKey("dbo.Stations", "RegionId", "dbo.Regions", "RegionId", cascadeDelete: true);
            AddForeignKey("dbo.Passengers", "RouteId", "dbo.Routes", "RouteId", cascadeDelete: true);
            AddForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners", "OwnerId", cascadeDelete: true);
            AddForeignKey("dbo.Passengers", "BoardingStationId", "dbo.Stations", "StationId", cascadeDelete: true);
            AddForeignKey("dbo.Regions", "StateId", "dbo.States", "StateId", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "ProviderId", "dbo.Providers", "ProviderId", cascadeDelete: true);
            AddForeignKey("dbo.Exports", "RouteId", "dbo.Routes", "RouteId", cascadeDelete: true);
        }
    }
}
