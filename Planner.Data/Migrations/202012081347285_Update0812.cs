namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update0812 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "BusTypeId", "dbo.BusTypes");
            DropIndex("dbo.Routes", new[] { "BusTypeId" });
            AddColumn("dbo.Routes", "BoardingRoute", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stations", "BoardingStation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stations", "StateId", c => c.Int());
            AddColumn("dbo.Stations", "RegionId", c => c.Int());
            AlterColumn("dbo.Routes", "BusTypeId", c => c.Int());
            CreateIndex("dbo.Routes", "BusTypeId");
            CreateIndex("dbo.Stations", "StateId");
            CreateIndex("dbo.Stations", "RegionId");
            AddForeignKey("dbo.Stations", "RegionId", "dbo.Regions", "RegionId");
            AddForeignKey("dbo.Stations", "StateId", "dbo.States", "StateId");
            AddForeignKey("dbo.Routes", "BusTypeId", "dbo.BusTypes", "BusTypeId");
            DropColumn("dbo.Stations", "IsInCze");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stations", "IsInCze", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Routes", "BusTypeId", "dbo.BusTypes");
            DropForeignKey("dbo.Stations", "StateId", "dbo.States");
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropIndex("dbo.Stations", new[] { "RegionId" });
            DropIndex("dbo.Stations", new[] { "StateId" });
            DropIndex("dbo.Routes", new[] { "BusTypeId" });
            AlterColumn("dbo.Routes", "BusTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Stations", "RegionId");
            DropColumn("dbo.Stations", "StateId");
            DropColumn("dbo.Stations", "BoardingStation");
            DropColumn("dbo.Routes", "BoardingRoute");
            CreateIndex("dbo.Routes", "BusTypeId");
            AddForeignKey("dbo.Routes", "BusTypeId", "dbo.BusTypes", "BusTypeId", cascadeDelete: true);
        }
    }
}
