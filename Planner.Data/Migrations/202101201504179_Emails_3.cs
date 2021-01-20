namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emails_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailTemplates", "Subject", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailTemplates", "Subject");
        }
    }
}
