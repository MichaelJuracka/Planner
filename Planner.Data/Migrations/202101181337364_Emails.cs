namespace Planner.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailAttachments",
                c => new
                    {
                        EmailAttachmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Data = c.Binary(nullable: false),
                        EmailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmailAttachmentId)
                .ForeignKey("dbo.Emails", t => t.EmailId)
                .Index(t => t.EmailId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        DateSent = c.DateTime(nullable: false),
                        Body = c.String(nullable: false),
                        EmailUserId = c.Int(nullable: false),
                        EmailTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.EmailTemplates", t => t.EmailTemplateId)
                .ForeignKey("dbo.EmailUsers", t => t.EmailUserId)
                .Index(t => t.EmailUserId)
                .Index(t => t.EmailTemplateId);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        EmailTemplateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Body = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmailTemplateId);
            
            CreateTable(
                "dbo.EmailUsers",
                c => new
                    {
                        EmailUserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        PassWord = c.String(nullable: false),
                        SmtpServer = c.String(nullable: false),
                        SmtpPort = c.Int(nullable: false),
                        Default = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmailUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailAttachments", "EmailId", "dbo.Emails");
            DropForeignKey("dbo.Emails", "EmailUserId", "dbo.EmailUsers");
            DropForeignKey("dbo.Emails", "EmailTemplateId", "dbo.EmailTemplates");
            DropIndex("dbo.Emails", new[] { "EmailTemplateId" });
            DropIndex("dbo.Emails", new[] { "EmailUserId" });
            DropIndex("dbo.EmailAttachments", new[] { "EmailId" });
            DropTable("dbo.EmailUsers");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.Emails");
            DropTable("dbo.EmailAttachments");
        }
    }
}
