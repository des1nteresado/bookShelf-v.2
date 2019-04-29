namespace FirstApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniverMigrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "AvMark", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "AvMark");
        }
    }
}
