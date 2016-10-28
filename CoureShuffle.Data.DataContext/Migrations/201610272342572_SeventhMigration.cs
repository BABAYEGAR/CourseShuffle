namespace CoureShuffle.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventhMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "SubscriptionStartDate", c => c.DateTime());
            AddColumn("dbo.Departments", "SubscriptionEndDate", c => c.DateTime());
            AddColumn("dbo.Departments", "SubscriptionType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "SubscriptionType");
            DropColumn("dbo.Departments", "SubscriptionEndDate");
            DropColumn("dbo.Departments", "SubscriptionStartDate");
        }
    }
}
