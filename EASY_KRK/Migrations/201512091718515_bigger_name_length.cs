namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bigger_name_length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Obszary", "NazwaObszaru", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Obszary", "NazwaObszaru", c => c.String(maxLength: 40));
        }
    }
}
