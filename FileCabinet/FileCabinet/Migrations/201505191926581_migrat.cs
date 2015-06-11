namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        UserProfileId = c.Int(nullable: false),
                        Title = c.String(),
                        PathToContent = c.String(),
                        ContentType = c.Int(nullable: false),
                        DateOfPublication = c.String(),
                    })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId);

            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 56),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "UserProfileId", "dbo.UserProfile");
            DropIndex("dbo.Articles", new[] { "UserProfileId" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.Articles");
        }
    }
}
