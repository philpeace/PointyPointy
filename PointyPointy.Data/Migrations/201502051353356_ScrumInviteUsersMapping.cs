using System.Data.Entity.Migrations;

namespace PointyPointy.Data.Migrations
{
    public partial class ScrumInviteUsersMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumInviteUsers", "Invite_Id", c => c.Int(false));
            CreateIndex("dbo.ScrumInviteUsers", "Invite_Id");
            AddForeignKey("dbo.ScrumInviteUsers", "Invite_Id", "dbo.ScrumInvites", "Id", true);
            DropColumn("dbo.ScrumInviteUsers", "ScrumInviteId");
        }

        public override void Down()
        {
            AddColumn("dbo.ScrumInviteUsers", "ScrumInviteId", c => c.Int(false));
            DropForeignKey("dbo.ScrumInviteUsers", "Invite_Id", "dbo.ScrumInvites");
            DropIndex("dbo.ScrumInviteUsers", new[] {"Invite_Id"});
            DropColumn("dbo.ScrumInviteUsers", "Invite_Id");
        }
    }
}