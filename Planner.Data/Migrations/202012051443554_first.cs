namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        PassengerId = c.Int(nullable: false, identity: true),
                        BusinessCase = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        SecondName = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(),
                        AdditionalInformation = c.String(maxLength: 2048),
                        RouteId = c.Int(nullable: false),
                        BoardingRouteId = c.Int(),
                        BoardingStationId = c.Int(nullable: false),
                        ExitStationId = c.Int(),
                        IsCleared = c.Boolean(nullable: false),
                        SeatNumber = c.Int(),
                    })
                .PrimaryKey(t => t.PassengerId)
                .ForeignKey("dbo.Routes", t => t.BoardingRouteId)
                .ForeignKey("dbo.Stations", t => t.BoardingStationId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.ExitStationId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId)
                .Index(t => t.BoardingRouteId)
                .Index(t => t.BoardingStationId)
                .Index(t => t.ExitStationId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        DepartureDate = c.DateTime(nullable: false),
                        RouteBack = c.Boolean(nullable: false),
                        RegionId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        AgendaCreated = c.Boolean(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        LicensePlate = c.String(),
                        OrderCreated = c.Boolean(nullable: false),
                        BusTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.BusTypes", t => t.BusTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.ProviderId)
                .Index(t => t.BusTypeId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 2048),
                    })
                .PrimaryKey(t => t.ProviderId);
            
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        RouteId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RouteId, t.StationId })
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.RouteId)
                .Index(t => t.StationId);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationId = c.Int(nullable: false, identity: true),
                        DepartureTime = c.DateTime(),
                        DeparturePlace = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 2048),
                        Gps = c.String(maxLength: 100),
                        IsInCze = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StationId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegionId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regions", "StateId", "dbo.States");
            DropForeignKey("dbo.Passengers", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Passengers", "ExitStationId", "dbo.Stations");
            DropForeignKey("dbo.Passengers", "BoardingStationId", "dbo.Stations");
            DropForeignKey("dbo.Passengers", "BoardingRouteId", "dbo.Routes");
            DropForeignKey("dbo.RouteStations", "StationId", "dbo.Stations");
            DropForeignKey("dbo.RouteStations", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Routes", "BusTypeId", "dbo.BusTypes");
            DropIndex("dbo.Regions", new[] { "StateId" });
            DropIndex("dbo.RouteStations", new[] { "StationId" });
            DropIndex("dbo.RouteStations", new[] { "RouteId" });
            DropIndex("dbo.Routes", new[] { "BusTypeId" });
            DropIndex("dbo.Routes", new[] { "ProviderId" });
            DropIndex("dbo.Passengers", new[] { "ExitStationId" });
            DropIndex("dbo.Passengers", new[] { "BoardingStationId" });
            DropIndex("dbo.Passengers", new[] { "BoardingRouteId" });
            DropIndex("dbo.Passengers", new[] { "RouteId" });
            DropTable("dbo.States");
            DropTable("dbo.Regions");
            DropTable("dbo.Stations");
            DropTable("dbo.RouteStations");
            DropTable("dbo.Providers");
            DropTable("dbo.Routes");
            DropTable("dbo.Passengers");
        }
    }
}
