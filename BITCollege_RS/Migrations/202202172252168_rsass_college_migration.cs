namespace BITCollege_RS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rsass_college_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentCards",
                c => new
                    {
                        StudentCardId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CardNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.StudentCardId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.NextUniqueNumbers",
                c => new
                    {
                        NextUniqueNumberId = c.Int(nullable: false, identity: true),
                        NextAvailableNumber = c.Long(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.NextUniqueNumberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCards", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentCards", new[] { "StudentId" });
            DropTable("dbo.NextUniqueNumbers");
            DropTable("dbo.StudentCards");
        }
    }
}
