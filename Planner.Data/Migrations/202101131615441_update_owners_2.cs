namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_owners_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Owners", "Email", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Owners", "Email");
        }
    }
}
