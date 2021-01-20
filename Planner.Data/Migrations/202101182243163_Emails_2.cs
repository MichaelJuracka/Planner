namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emails_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "Subject", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emails", "Subject");
        }
    }
}
