namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Export : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exports", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exports", "Created");
        }
    }
}
