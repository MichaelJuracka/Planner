namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passenger_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "DepartureTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passengers", "DepartureTime");
        }
    }
}
