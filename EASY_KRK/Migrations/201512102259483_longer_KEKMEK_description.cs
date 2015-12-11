namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class longer_KEKMEK_description : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KEKI", "Opis", c => c.String(maxLength: 400));
            AlterColumn("dbo.MEKI", "Opis", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MEKI", "Opis", c => c.String(maxLength: 150));
            AlterColumn("dbo.KEKI", "Opis", c => c.String(maxLength: 150));
        }
    }
}
