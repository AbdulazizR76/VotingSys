namespace VotingSys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VoteOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptionText = c.String(),
                        VoteId = c.Int(nullable: false),
                        VoteCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Votes", t => t.VoteId, cascadeDelete: true)
                .Index(t => t.VoteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoteOptions", "VoteId", "dbo.Votes");
            DropIndex("dbo.VoteOptions", new[] { "VoteId" });
            DropTable("dbo.VoteOptions");
            DropTable("dbo.Votes");
        }
    }
}
