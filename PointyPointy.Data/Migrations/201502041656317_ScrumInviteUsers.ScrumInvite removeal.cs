namespace PointyPointy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScrumInviteUsersScrumInviteremoveal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScrumInviteUsers", "Id", "dbo.ScrumInvites");
            DropIndex("dbo.ScrumInviteUsers", new[] { "Id" });
            DropPrimaryKey("dbo.ScrumInviteUsers");
            AddColumn("dbo.ScrumInviteUsers", "ScrumInviteId", c => c.Int(nullable: false));
            AlterColumn("dbo.ScrumInviteUsers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ScrumInviteUsers", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ScrumInviteUsers");
            AlterColumn("dbo.ScrumInviteUsers", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.ScrumInviteUsers", "ScrumInviteId");
            AddPrimaryKey("dbo.ScrumInviteUsers", "Id");
            CreateIndex("dbo.ScrumInviteUsers", "Id");
            AddForeignKey("dbo.ScrumInviteUsers", "Id", "dbo.ScrumInvites", "Id");
        }
    }
}
