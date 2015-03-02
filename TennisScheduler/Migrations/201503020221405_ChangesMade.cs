namespace TennisScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesMade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "CourtId_Id", "dbo.Courts");
            DropIndex("dbo.Reservations", new[] { "CourtId_Id" });
            RenameColumn(table: "dbo.Reservations", name: "CourtId_Id", newName: "CourtId");
            AddColumn("dbo.ClosedTimes", "EventName", c => c.String());
            AlterColumn("dbo.Reservations", "CourtId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "CourtId");
            AddForeignKey("dbo.Reservations", "CourtId", "dbo.Courts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "CourtId", "dbo.Courts");
            DropIndex("dbo.Reservations", new[] { "CourtId" });
            AlterColumn("dbo.Reservations", "CourtId", c => c.Int());
            DropColumn("dbo.ClosedTimes", "EventName");
            RenameColumn(table: "dbo.Reservations", name: "CourtId", newName: "CourtId_Id");
            CreateIndex("dbo.Reservations", "CourtId_Id");
            AddForeignKey("dbo.Reservations", "CourtId_Id", "dbo.Courts", "Id");
        }
    }
}
