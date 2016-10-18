namespace CoureShuffle.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigrationOne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "CreatedBy", c => c.Long(nullable: false));
            AddColumn("dbo.Departments", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Departments", "DateLastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Departments", "LastModifiedBy", c => c.Long(nullable: false));
            AddColumn("dbo.Faculties", "CreatedBy", c => c.Long(nullable: false));
            AddColumn("dbo.Faculties", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Faculties", "DateLastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Faculties", "LastModifiedBy", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faculties", "LastModifiedBy");
            DropColumn("dbo.Faculties", "DateLastModified");
            DropColumn("dbo.Faculties", "DateCreated");
            DropColumn("dbo.Faculties", "CreatedBy");
            DropColumn("dbo.Departments", "LastModifiedBy");
            DropColumn("dbo.Departments", "DateLastModified");
            DropColumn("dbo.Departments", "DateCreated");
            DropColumn("dbo.Departments", "CreatedBy");
        }
    }
}
