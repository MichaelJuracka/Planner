namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update0812_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stations", "StateId", "dbo.States");
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropIndex("dbo.Stations", new[] { "StateId" });
            DropIndex("dbo.Stations", new[] { "RegionId" });
            AlterColumn("dbo.Stations", "RegionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Stations", "RegionId");
            AddForeignKey("dbo.Stations", "RegionId", "dbo.Regions", "RegionId", cascadeDelete: true);
            DropColumn("dbo.Stations", "StateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stations", "StateId", c => c.Int());
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropIndex("dbo.Stations", new[] { "RegionId" });
            AlterColumn("dbo.Stations", "RegionId", c => c.Int());
            CreateIndex("dbo.Stations", "RegionId");
            CreateIndex("dbo.Stations", "StateId");
            AddForeignKey("dbo.Stations", "RegionId", "dbo.Regions", "RegionId");
            AddForeignKey("dbo.Stations", "StateId", "dbo.States", "StateId");
        }
    }
}
