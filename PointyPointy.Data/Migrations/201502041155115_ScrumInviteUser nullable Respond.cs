using System.Data.Entity.Migrations;

namespace PointyPointy.Data.Migrations
{
    public partial class ScrumInviteUsernullableRespond : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ScrumInviteUsers", "Responded", c => c.DateTime());
        }

        public override void Down()
        {
            AlterColumn("dbo.ScrumInviteUsers", "Responded", c => c.DateTime(false));
        }
    }
}