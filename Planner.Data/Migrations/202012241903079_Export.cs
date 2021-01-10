namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Export : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exports",
                c => new
                    {
                        ExportId = c.Int(nullable: false, identity: true),
                        Data = c.Binary(),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExportId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exports", "RouteId", "dbo.Routes");
            DropIndex("dbo.Exports", new[] { "RouteId" });
            DropTable("dbo.Exports");
        }
    }
}
