using System.Data.Entity.Migrations;

namespace PointyPointy.Data.Migrations
{
    public partial class ScrumInvites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScrumInvites",
                c => new
                {
                    Id = c.Int(false, true),
                    UserId = c.String(false),
                    Created = c.DateTime(false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ScrumInviteUsers",
                c => new
                {
                    Id = c.Int(false, true),
                    UserId = c.String(false),
                    Responded = c.DateTime(false),
                    Accepted = c.Boolean(false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScrumInvites", t => t.Id)
                .Index(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ScrumInviteUsers", "Id", "dbo.ScrumInvites");
            DropIndex("dbo.ScrumInviteUsers", new[] {"Id"});
            DropTable("dbo.ScrumInviteUsers");
            DropTable("dbo.ScrumInvites");
        }
    }
}