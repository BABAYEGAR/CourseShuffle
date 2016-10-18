namespace CoureShuffle.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        FacultyId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        FacultyId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FacultyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "FacultyId" });
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
        }
    }
}
