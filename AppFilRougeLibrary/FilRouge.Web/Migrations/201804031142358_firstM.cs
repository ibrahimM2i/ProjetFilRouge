namespace FilRouge.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quizzs", "DifficultyMasterId", "dbo.DifficultyMasters");
            DropIndex("dbo.Quizzs", new[] { "DifficultyMasterId" });
            AddColumn("dbo.Quizzs", "DifficultyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quizzs", "DifficultyId");
            AddForeignKey("dbo.Quizzs", "DifficultyId", "dbo.Difficulties", "DifficultyId");
            DropColumn("dbo.Quizzs", "DifficultyMasterId");
            DropTable("dbo.DifficultyMasters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DifficultyMasters",
                c => new
                    {
                        DiffMasterId = c.Int(nullable: false, identity: true),
                        DiffMasterName = c.String(),
                    })
                .PrimaryKey(t => t.DiffMasterId);
            
            AddColumn("dbo.Quizzs", "DifficultyMasterId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Quizzs", "DifficultyId", "dbo.Difficulties");
            DropIndex("dbo.Quizzs", new[] { "DifficultyId" });
            DropColumn("dbo.Quizzs", "DifficultyId");
            CreateIndex("dbo.Quizzs", "DifficultyMasterId");
            AddForeignKey("dbo.Quizzs", "DifficultyMasterId", "dbo.DifficultyMasters", "DiffMasterId");
        }
    }
}
