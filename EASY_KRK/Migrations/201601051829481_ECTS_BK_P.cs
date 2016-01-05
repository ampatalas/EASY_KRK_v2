namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ECTS_BK_P : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kursy", "ECTS_P", c => c.Single(nullable: false));
            AlterColumn("dbo.Kursy", "ECTS_BK", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kursy", "ECTS_BK", c => c.Int(nullable: false));
            AlterColumn("dbo.Kursy", "ECTS_P", c => c.Int(nullable: false));
        }
    }
}
