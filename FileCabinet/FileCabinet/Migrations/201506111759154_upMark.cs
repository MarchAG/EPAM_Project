namespace FileCabinet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upMark : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Marks", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Marks", "MyProperty", c => c.Int(nullable: false));
        }
    }
}
