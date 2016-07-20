namespace ItemMaster.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReasonCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemHistories", "ReasonCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemHistories", "ReasonCode");
        }
    }
}
