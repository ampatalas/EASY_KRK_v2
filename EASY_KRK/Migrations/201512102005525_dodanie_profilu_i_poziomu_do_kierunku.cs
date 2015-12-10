namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanie_profilu_i_poziomu_do_kierunku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kierunki", "IdPoziomu", c => c.Int());
            AddColumn("dbo.Kierunki", "IdProfilu", c => c.Int());
            CreateIndex("dbo.Kierunki", "IdPoziomu");
            CreateIndex("dbo.Kierunki", "IdProfilu");
            AddForeignKey("dbo.Kierunki", "IdPoziomu", "dbo.Poziomy", "IdPoziomu");
            AddForeignKey("dbo.Kierunki", "IdProfilu", "dbo.Profile", "IdProfilu");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kierunki", "IdProfilu", "dbo.Profile");
            DropForeignKey("dbo.Kierunki", "IdPoziomu", "dbo.Poziomy");
            DropIndex("dbo.Kierunki", new[] { "IdProfilu" });
            DropIndex("dbo.Kierunki", new[] { "IdPoziomu" });
            DropColumn("dbo.Kierunki", "IdProfilu");
            DropColumn("dbo.Kierunki", "IdPoziomu");
        }
    }
}
