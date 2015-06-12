namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        MarkId = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MarkId)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: false)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId, cascadeDelete: false)
                .Index(t => t.ArticleId)
                .Index(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.Marks", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Marks", new[] { "UserProfileId" });
            DropIndex("dbo.Marks", new[] { "ArticleId" });
            DropTable("dbo.Marks");
        }
    }
}
