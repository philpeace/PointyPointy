namespace PointyPointy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScrumInviteUseraddemail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumInviteUsers", "Email", c => c.String(nullable: false));
            DropColumn("dbo.ScrumInviteUsers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScrumInviteUsers", "UserId", c => c.String(nullable: false));
            DropColumn("dbo.ScrumInviteUsers", "Email");
        }
    }
}
