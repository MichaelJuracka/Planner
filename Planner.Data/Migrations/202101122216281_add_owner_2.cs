namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_owner_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners");
            DropIndex("dbo.Passengers", new[] { "OwnerId" });
            AlterColumn("dbo.Passengers", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Passengers", "OwnerId");
            AddForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners", "OwnerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners");
            DropIndex("dbo.Passengers", new[] { "OwnerId" });
            AlterColumn("dbo.Passengers", "OwnerId", c => c.Int());
            CreateIndex("dbo.Passengers", "OwnerId");
            AddForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners", "OwnerId");
        }
    }
}
