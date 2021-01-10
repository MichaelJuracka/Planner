namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class route_update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Routes", "RegionId", c => c.Int());
            AlterColumn("dbo.Routes", "StateId", c => c.Int());
            CreateIndex("dbo.Routes", "StateId");
            AddForeignKey("dbo.Routes", "StateId", "dbo.States", "StateId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "StateId", "dbo.States");
            DropIndex("dbo.Routes", new[] { "StateId" });
            AlterColumn("dbo.Routes", "StateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Routes", "RegionId", c => c.Int(nullable: false));
        }
    }
}
