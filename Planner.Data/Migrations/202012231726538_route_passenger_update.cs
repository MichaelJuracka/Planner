namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class route_passenger_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "RealRouteId", c => c.Int());
            AddColumn("dbo.Routes", "IsRealRoute", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Passengers", "RealRouteId");
            AddForeignKey("dbo.Passengers", "RealRouteId", "dbo.Routes", "RouteId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passengers", "RealRouteId", "dbo.Routes");
            DropIndex("dbo.Passengers", new[] { "RealRouteId" });
            DropColumn("dbo.Routes", "IsRealRoute");
            DropColumn("dbo.Passengers", "RealRouteId");
        }
    }
}
