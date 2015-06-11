namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Description", c => c.String());
            DropColumn("dbo.Articles", "Discription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Discription", c => c.String());
            DropColumn("dbo.Articles", "Description");
        }
    }
}
