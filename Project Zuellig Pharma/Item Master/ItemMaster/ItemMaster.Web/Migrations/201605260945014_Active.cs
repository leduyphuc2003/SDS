namespace ItemMaster.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Active : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ZppStocks", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ZppStocks", "Active");
        }
    }
}
