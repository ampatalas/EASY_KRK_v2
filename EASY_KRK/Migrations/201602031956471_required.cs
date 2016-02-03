namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FormyPrzedmiotu", "NazwaFormy", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.FormyStudiow", "NazwaFormyStudiow", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.FormyZajec", "NazwaFormy", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.FormyZal", "NazwaFormyZal", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.GrupyKursow", "KodGrupyKursow", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Przedmioty", "KodPrzedmiotu", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Przedmioty", "NazwaPrzedmiotu", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Kategorie", "NazwaKategorii", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.JezykiStudiow", "NazwaJezyka", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Kierunki", "NazwaKierunku", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Poziomy", "NazwaPoziomu", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Profile", "NazwaProfilu", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.RodzajeStudiow", "NazwaRodzajuStudiow", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.RodzajePrzedmiotu", "NazwaRodzaju", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.TypyPrzedmiotu", "NazwaTypu", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.KEKI", "Kod", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.KEKI", "Opis", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.MEKI", "Kod", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.MEKI", "Opis", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.Obszary", "NazwaObszaru", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Obszary", "NazwaObszaru", c => c.String(maxLength: 100));
            AlterColumn("dbo.MEKI", "Opis", c => c.String(maxLength: 400));
            AlterColumn("dbo.MEKI", "Kod", c => c.String(maxLength: 40));
            AlterColumn("dbo.KEKI", "Opis", c => c.String(maxLength: 400));
            AlterColumn("dbo.KEKI", "Kod", c => c.String(maxLength: 40));
            AlterColumn("dbo.TypyPrzedmiotu", "NazwaTypu", c => c.String(maxLength: 40));
            AlterColumn("dbo.RodzajePrzedmiotu", "NazwaRodzaju", c => c.String(maxLength: 40));
            AlterColumn("dbo.RodzajeStudiow", "NazwaRodzajuStudiow", c => c.String(maxLength: 40));
            AlterColumn("dbo.Profile", "NazwaProfilu", c => c.String(maxLength: 40));
            AlterColumn("dbo.Poziomy", "NazwaPoziomu", c => c.String(maxLength: 40));
            AlterColumn("dbo.Kierunki", "NazwaKierunku", c => c.String(maxLength: 40));
            AlterColumn("dbo.JezykiStudiow", "NazwaJezyka", c => c.String(maxLength: 40));
            AlterColumn("dbo.Kategorie", "NazwaKategorii", c => c.String(maxLength: 100));
            AlterColumn("dbo.Przedmioty", "NazwaPrzedmiotu", c => c.String(maxLength: 100));
            AlterColumn("dbo.Przedmioty", "KodPrzedmiotu", c => c.String(maxLength: 15));
            AlterColumn("dbo.GrupyKursow", "KodGrupyKursow", c => c.String(maxLength: 20));
            AlterColumn("dbo.FormyZal", "NazwaFormyZal", c => c.String(maxLength: 40));
            AlterColumn("dbo.FormyZajec", "NazwaFormy", c => c.String(maxLength: 40));
            AlterColumn("dbo.FormyStudiow", "NazwaFormyStudiow", c => c.String(maxLength: 40));
            AlterColumn("dbo.FormyPrzedmiotu", "NazwaFormy", c => c.String(maxLength: 40));
        }
    }
}
