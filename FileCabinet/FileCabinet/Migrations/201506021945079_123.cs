namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "FileName", c => c.String());
            DropColumn("dbo.Articles", "PathToContent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "PathToContent", c => c.String());
            DropColumn("dbo.Articles", "FileName");
        }
    }
}
