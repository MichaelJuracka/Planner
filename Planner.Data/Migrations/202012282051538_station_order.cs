namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class station_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stations", "order");
        }
    }
}
