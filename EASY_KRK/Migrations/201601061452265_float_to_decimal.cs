namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class float_to_decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kursy", "ECTS_P", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kursy", "ECTS_P", c => c.Single(nullable: false));
        }
    }
}
