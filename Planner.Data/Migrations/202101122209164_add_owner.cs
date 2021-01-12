namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_owner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.OwnerId);
            
            AddColumn("dbo.Passengers", "OwnerId", c => c.Int());
            CreateIndex("dbo.Passengers", "OwnerId");
            AddForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners", "OwnerId");
            DropColumn("dbo.Passengers", "Owner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passengers", "Owner", c => c.String());
            DropForeignKey("dbo.Passengers", "OwnerId", "dbo.Owners");
            DropIndex("dbo.Passengers", new[] { "OwnerId" });
            DropColumn("dbo.Passengers", "OwnerId");
            DropTable("dbo.Owners");
        }
    }
}
