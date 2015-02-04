namespace PointyPointy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScrumInviteUsernullableRespond : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ScrumInviteUsers", "Responded", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScrumInviteUsers", "Responded", c => c.DateTime(nullable: false));
        }
    }
}
