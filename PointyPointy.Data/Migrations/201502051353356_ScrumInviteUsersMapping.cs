namespace PointyPointy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScrumInviteUsersMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumInviteUsers", "Invite_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ScrumInviteUsers", "Invite_Id");
            AddForeignKey("dbo.ScrumInviteUsers", "Invite_Id", "dbo.ScrumInvites", "Id", cascadeDelete: true);
            DropColumn("dbo.ScrumInviteUsers", "ScrumInviteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScrumInviteUsers", "ScrumInviteId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ScrumInviteUsers", "Invite_Id", "dbo.ScrumInvites");
            DropIndex("dbo.ScrumInviteUsers", new[] { "Invite_Id" });
            DropColumn("dbo.ScrumInviteUsers", "Invite_Id");
        }
    }
}
