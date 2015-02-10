using System.Data.Entity.Migrations;

namespace PointyPointy.Data.Migrations
{
    public partial class ScrumInviteUseraddemail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrumInviteUsers", "Email", c => c.String(false));
            DropColumn("dbo.ScrumInviteUsers", "UserId");
        }

        public override void Down()
        {
            AddColumn("dbo.ScrumInviteUsers", "UserId", c => c.String(false));
            DropColumn("dbo.ScrumInviteUsers", "Email");
        }
    }
}