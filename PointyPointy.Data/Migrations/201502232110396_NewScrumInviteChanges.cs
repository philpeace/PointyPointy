namespace PointyPointy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewScrumInviteChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumInviteUsers", "RequestKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrumInviteUsers", "RequestKey");
        }
    }
}
