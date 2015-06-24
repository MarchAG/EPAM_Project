namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniq : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Marks", new[] { "ArticleId" });
            DropIndex("dbo.Marks", new[] { "UserProfileId" });
            CreateIndex("dbo.Marks", new[] { "ArticleId", "UserProfileId" }, unique: true, name: "IX_UserAndArticle");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Marks", "IX_UserAndArticle");
            CreateIndex("dbo.Marks", "UserProfileId");
            CreateIndex("dbo.Marks", "ArticleId");
        }
    }
}
