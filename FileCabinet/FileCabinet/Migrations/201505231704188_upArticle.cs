namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Mark", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Mark");
        }
    }
}
