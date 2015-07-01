namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagArticles",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Article_ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Article_ArticleId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Article_ArticleId);
            
            DropColumn("dbo.Articles", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Tags", c => c.String());
            DropForeignKey("dbo.TagArticles", "Article_ArticleId", "dbo.Articles");
            DropForeignKey("dbo.TagArticles", "Tag_TagId", "dbo.Tags");
            DropIndex("dbo.TagArticles", new[] { "Article_ArticleId" });
            DropIndex("dbo.TagArticles", new[] { "Tag_TagId" });
            DropTable("dbo.TagArticles");
            DropTable("dbo.Tags");
        }
    }
}
