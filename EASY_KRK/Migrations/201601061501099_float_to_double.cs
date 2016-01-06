namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class float_to_double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GrupyKursow", "ECTS_P", c => c.Double(nullable: false));
            AlterColumn("dbo.GrupyKursow", "ECTS_BK", c => c.Double(nullable: false));
            AlterColumn("dbo.Kursy", "ECTS_P", c => c.Double(nullable: false));
            AlterColumn("dbo.Kursy", "ECTS_BK", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kursy", "ECTS_BK", c => c.Single(nullable: false));
            AlterColumn("dbo.Kursy", "ECTS_P", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.GrupyKursow", "ECTS_BK", c => c.Single(nullable: false));
            AlterColumn("dbo.GrupyKursow", "ECTS_P", c => c.Single(nullable: false));
        }
    }
}
