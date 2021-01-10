namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Export_Name : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exports", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exports", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
