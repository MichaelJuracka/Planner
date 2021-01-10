namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class route_update_2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Routes", "RegionId");
            AddForeignKey("dbo.Routes", "RegionId", "dbo.Regions", "RegionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "RegionId", "dbo.Regions");
            DropIndex("dbo.Routes", new[] { "RegionId" });
        }
    }
}
