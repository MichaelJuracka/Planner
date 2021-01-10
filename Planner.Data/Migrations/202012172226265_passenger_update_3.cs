namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passenger_update_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "Owner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passengers", "Owner");
        }
    }
}
