namespace EASY_KRK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormyPrzedmiotu",
                c => new
                    {
                        IdFormyPrzedmiotu = c.Int(nullable: false, identity: true),
                        NazwaFormy = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdFormyPrzedmiotu);
            
            CreateTable(
                "dbo.FormyStudiow",
                c => new
                    {
                        IdFormyStudiow = c.Int(nullable: false, identity: true),
                        NazwaFormyStudiow = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdFormyStudiow);
            
            CreateTable(
                "dbo.FormyZajec",
                c => new
                    {
                        IdFormyZajec = c.Int(nullable: false, identity: true),
                        NazwaFormy = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdFormyZajec);
            
            CreateTable(
                "dbo.FormyZal",
                c => new
                    {
                        IdFormyZal = c.Int(nullable: false, identity: true),
                        NazwaFormyZal = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdFormyZal);
            
            CreateTable(
                "dbo.GrupyKursow",
                c => new
                    {
                        IdGrupyKursow = c.Int(nullable: false, identity: true),
                        KodGrupyKursow = c.String(maxLength: 20),
                        PunktyECTS = c.Int(nullable: false),
                        ZZU = c.Int(nullable: false),
                        CNPS = c.Int(nullable: false),
                        ECTS_P = c.Int(nullable: false),
                        ECTS_BK = c.Int(nullable: false),
                        Praktyczny = c.Boolean(nullable: false),
                        IdPrzedmiotu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdGrupyKursow)
                .ForeignKey("dbo.Przedmioty", t => t.IdPrzedmiotu, cascadeDelete: true)
                .Index(t => t.IdPrzedmiotu);
            
            CreateTable(
                "dbo.Przedmioty",
                c => new
                    {
                        IdPrzedmiotu = c.Int(nullable: false, identity: true),
                        KodPrzedmiotu = c.String(maxLength: 15),
                        NazwaPrzedmiotu = c.String(maxLength: 100),
                        IdRodzajuPrzedmiotu = c.Int(nullable: false),
                        IdFormyPrzedmiotu = c.Int(nullable: false),
                        IdTypuPrzedmiotu = c.Int(nullable: false),
                        IdKategorii = c.Int(nullable: false),
                        Ogolnouczelniany = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdPrzedmiotu)
                .ForeignKey("dbo.FormyPrzedmiotu", t => t.IdFormyPrzedmiotu, cascadeDelete: true)
                .ForeignKey("dbo.Kategorie", t => t.IdKategorii, cascadeDelete: true)
                .ForeignKey("dbo.RodzajePrzedmiotu", t => t.IdRodzajuPrzedmiotu, cascadeDelete: true)
                .ForeignKey("dbo.TypyPrzedmiotu", t => t.IdTypuPrzedmiotu, cascadeDelete: true)
                .Index(t => t.IdFormyPrzedmiotu)
                .Index(t => t.IdKategorii)
                .Index(t => t.IdRodzajuPrzedmiotu)
                .Index(t => t.IdTypuPrzedmiotu);
            
            CreateTable(
                "dbo.Kategorie",
                c => new
                    {
                        IdKategorii = c.Int(nullable: false, identity: true),
                        NazwaKategorii = c.String(maxLength: 100),
                        MinECTS = c.Int(),
                        IdProgramuStudiow = c.Int(nullable: false),
                        IdKategoriiNadrzednej = c.Int(),
                    })
                .PrimaryKey(t => t.IdKategorii)
                .ForeignKey("dbo.Kategorie", t => t.IdKategoriiNadrzednej)
                .ForeignKey("dbo.ProgramyStudiow", t => t.IdProgramuStudiow, cascadeDelete: true)
                .Index(t => t.IdKategoriiNadrzednej)
                .Index(t => t.IdProgramuStudiow);
            
            CreateTable(
                "dbo.ProgramyStudiow",
                c => new
                    {
                        IdProgramuStudiow = c.Int(nullable: false, identity: true),
                        IdProgramuKsztalcenia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProgramuStudiow)
                .ForeignKey("dbo.ProgramyKsztalcenia", t => t.IdProgramuKsztalcenia, cascadeDelete: true)
                .Index(t => t.IdProgramuKsztalcenia);
            
            CreateTable(
                "dbo.ProgramyKsztalcenia",
                c => new
                    {
                        IdProgramuKsztalcenia = c.Int(nullable: false, identity: true),
                        IdKierunku = c.Int(nullable: false),
                        IdFormyStudiow = c.Int(nullable: false),
                        IdJezykaStudiow = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProgramuKsztalcenia)
                .ForeignKey("dbo.FormyStudiow", t => t.IdFormyStudiow, cascadeDelete: true)
                .ForeignKey("dbo.JezykiStudiow", t => t.IdJezykaStudiow, cascadeDelete: true)
                .ForeignKey("dbo.Kierunki", t => t.IdKierunku, cascadeDelete: true)
                .Index(t => t.IdFormyStudiow)
                .Index(t => t.IdJezykaStudiow)
                .Index(t => t.IdKierunku);
            
            CreateTable(
                "dbo.JezykiStudiow",
                c => new
                    {
                        IdJezyka = c.Int(nullable: false, identity: true),
                        NazwaJezyka = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdJezyka);
            
            CreateTable(
                "dbo.Kierunki",
                c => new
                    {
                        IdKierunku = c.Int(nullable: false, identity: true),
                        NazwaKierunku = c.String(maxLength: 40),
                        IdRodzajuStudiow = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdKierunku)
                .ForeignKey("dbo.RodzajeStudiow", t => t.IdRodzajuStudiow, cascadeDelete: true)
                .Index(t => t.IdRodzajuStudiow);
            
            CreateTable(
                "dbo.RodzajeStudiow",
                c => new
                    {
                        IdRodzajuStudiow = c.Int(nullable: false, identity: true),
                        NazwaRodzajuStudiow = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdRodzajuStudiow);
            
            CreateTable(
                "dbo.RodzajePrzedmiotu",
                c => new
                    {
                        IdRodzaju = c.Int(nullable: false, identity: true),
                        NazwaRodzaju = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdRodzaju);
            
            CreateTable(
                "dbo.TypyPrzedmiotu",
                c => new
                    {
                        IdTypu = c.Int(nullable: false, identity: true),
                        NazwaTypu = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdTypu);
            
            CreateTable(
                "dbo.KEKI",
                c => new
                    {
                        IdKEK = c.Int(nullable: false, identity: true),
                        Kod = c.String(maxLength: 40),
                        Opis = c.String(maxLength: 150),
                        IdKierunku = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdKEK)
                .ForeignKey("dbo.Kierunki", t => t.IdKierunku, cascadeDelete: true)
                .Index(t => t.IdKierunku);
            
            CreateTable(
                "dbo.KEKIPrzedmiotow",
                c => new
                    {
                        IdKEKPrzedmiotu = c.Int(nullable: false, identity: true),
                        IdKEK = c.Int(nullable: false),
                        IdPrzedmiotu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdKEKPrzedmiotu)
                .ForeignKey("dbo.KEKI", t => t.IdKEK, cascadeDelete: true)
                .ForeignKey("dbo.Przedmioty", t => t.IdPrzedmiotu)
                .Index(t => t.IdKEK)
                .Index(t => t.IdPrzedmiotu);
            
            CreateTable(
                "dbo.Kursy",
                c => new
                    {
                        IdKursu = c.Int(nullable: false, identity: true),
                        KodKursu = c.String(maxLength: 16),
                        PunktyECTS = c.Int(nullable: false),
                        ZZU = c.Int(nullable: false),
                        CNPS = c.Int(nullable: false),
                        ECTS_P = c.Int(nullable: false),
                        ECTS_BK = c.Int(nullable: false),
                        Praktyczny = c.Boolean(nullable: false),
                        IdFormyZajec = c.Int(nullable: false),
                        IdFormyZal = c.Int(),
                        IdPrzedmiotu = c.Int(nullable: false),
                        IdGrupyKursow = c.Int(),
                    })
                .PrimaryKey(t => t.IdKursu)
                .ForeignKey("dbo.FormyZajec", t => t.IdFormyZajec, cascadeDelete: true)
                .ForeignKey("dbo.FormyZal", t => t.IdFormyZal)
                .ForeignKey("dbo.GrupyKursow", t => t.IdGrupyKursow)
                .ForeignKey("dbo.Przedmioty", t => t.IdPrzedmiotu, cascadeDelete: true)
                .Index(t => t.IdFormyZajec)
                .Index(t => t.IdFormyZal)
                .Index(t => t.IdGrupyKursow)
                .Index(t => t.IdPrzedmiotu);
            
            CreateTable(
                "dbo.MEKI",
                c => new
                    {
                        IdMEK = c.Int(nullable: false, identity: true),
                        Kod = c.String(maxLength: 40),
                        Opis = c.String(maxLength: 150),
                        IdObszaru = c.Int(nullable: false),
                        IdPoziomu = c.Int(nullable: false),
                        IdProfilu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdMEK)
                .ForeignKey("dbo.Obszary", t => t.IdObszaru, cascadeDelete: true)
                .ForeignKey("dbo.Poziomy", t => t.IdPoziomu, cascadeDelete: true)
                .ForeignKey("dbo.Profile", t => t.IdProfilu, cascadeDelete: true)
                .Index(t => t.IdObszaru)
                .Index(t => t.IdPoziomu)
                .Index(t => t.IdProfilu);
            
            CreateTable(
                "dbo.Obszary",
                c => new
                    {
                        IdObszaru = c.Int(nullable: false, identity: true),
                        NazwaObszaru = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdObszaru);
            
            CreateTable(
                "dbo.Poziomy",
                c => new
                    {
                        IdPoziomu = c.Int(nullable: false, identity: true),
                        NazwaPoziomu = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdPoziomu);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        IdProfilu = c.Int(nullable: false, identity: true),
                        NazwaProfilu = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.IdProfilu);
            
            CreateTable(
                "dbo.Sladowania",
                c => new
                    {
                        IdSladowania = c.Int(nullable: false, identity: true),
                        IdMEK = c.Int(nullable: false),
                        IdKEK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSladowania)
                .ForeignKey("dbo.KEKI", t => t.IdKEK, cascadeDelete: true)
                .ForeignKey("dbo.MEKI", t => t.IdMEK, cascadeDelete: true)
                .Index(t => t.IdKEK)
                .Index(t => t.IdMEK);
            
            CreateTable(
                "dbo.UdzialyProcentowe",
                c => new
                    {
                        IdUdzialu = c.Int(nullable: false, identity: true),
                        Wartosc = c.Double(nullable: false),
                        IdKierunku = c.Int(nullable: false),
                        IdObszaru = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUdzialu)
                .ForeignKey("dbo.Kierunki", t => t.IdKierunku, cascadeDelete: true)
                .ForeignKey("dbo.Obszary", t => t.IdObszaru, cascadeDelete: true)
                .Index(t => t.IdKierunku)
                .Index(t => t.IdObszaru);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UdzialyProcentowe", "IdObszaru", "dbo.Obszary");
            DropForeignKey("dbo.UdzialyProcentowe", "IdKierunku", "dbo.Kierunki");
            DropForeignKey("dbo.Sladowania", "IdMEK", "dbo.MEKI");
            DropForeignKey("dbo.Sladowania", "IdKEK", "dbo.KEKI");
            DropForeignKey("dbo.MEKI", "IdProfilu", "dbo.Profile");
            DropForeignKey("dbo.MEKI", "IdPoziomu", "dbo.Poziomy");
            DropForeignKey("dbo.MEKI", "IdObszaru", "dbo.Obszary");
            DropForeignKey("dbo.Kursy", "IdPrzedmiotu", "dbo.Przedmioty");
            DropForeignKey("dbo.Kursy", "IdGrupyKursow", "dbo.GrupyKursow");
            DropForeignKey("dbo.Kursy", "IdFormyZal", "dbo.FormyZal");
            DropForeignKey("dbo.Kursy", "IdFormyZajec", "dbo.FormyZajec");
            DropForeignKey("dbo.KEKIPrzedmiotow", "IdPrzedmiotu", "dbo.Przedmioty");
            DropForeignKey("dbo.KEKIPrzedmiotow", "IdKEK", "dbo.KEKI");
            DropForeignKey("dbo.KEKI", "IdKierunku", "dbo.Kierunki");
            DropForeignKey("dbo.GrupyKursow", "IdPrzedmiotu", "dbo.Przedmioty");
            DropForeignKey("dbo.Przedmioty", "IdTypuPrzedmiotu", "dbo.TypyPrzedmiotu");
            DropForeignKey("dbo.Przedmioty", "IdRodzajuPrzedmiotu", "dbo.RodzajePrzedmiotu");
            DropForeignKey("dbo.Przedmioty", "IdKategorii", "dbo.Kategorie");
            DropForeignKey("dbo.Kategorie", "IdProgramuStudiow", "dbo.ProgramyStudiow");
            DropForeignKey("dbo.ProgramyStudiow", "IdProgramuKsztalcenia", "dbo.ProgramyKsztalcenia");
            DropForeignKey("dbo.ProgramyKsztalcenia", "IdKierunku", "dbo.Kierunki");
            DropForeignKey("dbo.Kierunki", "IdRodzajuStudiow", "dbo.RodzajeStudiow");
            DropForeignKey("dbo.ProgramyKsztalcenia", "IdJezykaStudiow", "dbo.JezykiStudiow");
            DropForeignKey("dbo.ProgramyKsztalcenia", "IdFormyStudiow", "dbo.FormyStudiow");
            DropForeignKey("dbo.Kategorie", "IdKategoriiNadrzednej", "dbo.Kategorie");
            DropForeignKey("dbo.Przedmioty", "IdFormyPrzedmiotu", "dbo.FormyPrzedmiotu");
            DropIndex("dbo.UdzialyProcentowe", new[] { "IdObszaru" });
            DropIndex("dbo.UdzialyProcentowe", new[] { "IdKierunku" });
            DropIndex("dbo.Sladowania", new[] { "IdMEK" });
            DropIndex("dbo.Sladowania", new[] { "IdKEK" });
            DropIndex("dbo.MEKI", new[] { "IdProfilu" });
            DropIndex("dbo.MEKI", new[] { "IdPoziomu" });
            DropIndex("dbo.MEKI", new[] { "IdObszaru" });
            DropIndex("dbo.Kursy", new[] { "IdPrzedmiotu" });
            DropIndex("dbo.Kursy", new[] { "IdGrupyKursow" });
            DropIndex("dbo.Kursy", new[] { "IdFormyZal" });
            DropIndex("dbo.Kursy", new[] { "IdFormyZajec" });
            DropIndex("dbo.KEKIPrzedmiotow", new[] { "IdPrzedmiotu" });
            DropIndex("dbo.KEKIPrzedmiotow", new[] { "IdKEK" });
            DropIndex("dbo.KEKI", new[] { "IdKierunku" });
            DropIndex("dbo.GrupyKursow", new[] { "IdPrzedmiotu" });
            DropIndex("dbo.Przedmioty", new[] { "IdTypuPrzedmiotu" });
            DropIndex("dbo.Przedmioty", new[] { "IdRodzajuPrzedmiotu" });
            DropIndex("dbo.Przedmioty", new[] { "IdKategorii" });
            DropIndex("dbo.Kategorie", new[] { "IdProgramuStudiow" });
            DropIndex("dbo.ProgramyStudiow", new[] { "IdProgramuKsztalcenia" });
            DropIndex("dbo.ProgramyKsztalcenia", new[] { "IdKierunku" });
            DropIndex("dbo.Kierunki", new[] { "IdRodzajuStudiow" });
            DropIndex("dbo.ProgramyKsztalcenia", new[] { "IdJezykaStudiow" });
            DropIndex("dbo.ProgramyKsztalcenia", new[] { "IdFormyStudiow" });
            DropIndex("dbo.Kategorie", new[] { "IdKategoriiNadrzednej" });
            DropIndex("dbo.Przedmioty", new[] { "IdFormyPrzedmiotu" });
            DropTable("dbo.UdzialyProcentowe");
            DropTable("dbo.Sladowania");
            DropTable("dbo.Profile");
            DropTable("dbo.Poziomy");
            DropTable("dbo.Obszary");
            DropTable("dbo.MEKI");
            DropTable("dbo.Kursy");
            DropTable("dbo.KEKIPrzedmiotow");
            DropTable("dbo.KEKI");
            DropTable("dbo.TypyPrzedmiotu");
            DropTable("dbo.RodzajePrzedmiotu");
            DropTable("dbo.RodzajeStudiow");
            DropTable("dbo.Kierunki");
            DropTable("dbo.JezykiStudiow");
            DropTable("dbo.ProgramyKsztalcenia");
            DropTable("dbo.ProgramyStudiow");
            DropTable("dbo.Kategorie");
            DropTable("dbo.Przedmioty");
            DropTable("dbo.GrupyKursow");
            DropTable("dbo.FormyZal");
            DropTable("dbo.FormyZajec");
            DropTable("dbo.FormyStudiow");
            DropTable("dbo.FormyPrzedmiotu");
        }
    }
}
