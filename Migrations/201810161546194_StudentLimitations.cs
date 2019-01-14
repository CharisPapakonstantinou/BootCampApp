namespace BootCampApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentLimitations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "Lastname", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "Firstname", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "Firstname", c => c.String());
            AlterColumn("dbo.Student", "Lastname", c => c.String());
        }
    }
}
