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
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScrumInviteUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Responded = c.DateTime(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScrumInvites", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScrumInviteUsers", "Id", "dbo.ScrumInvites");
            DropIndex("dbo.ScrumInviteUsers", new[] { "Id" });
            DropTable("dbo.ScrumInviteUsers");
            DropTable("dbo.ScrumInvites");
        }
    }
}
