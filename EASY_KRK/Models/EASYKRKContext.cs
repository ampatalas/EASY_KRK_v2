using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    public class EASYKRKContext: DbContext
    {
        public EASYKRKContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<FormaZajec> FormyZajec { get; set; }
        public DbSet<RodzajPrzedmiotu> RodzajePrzedmioty { get; set; }
        public DbSet<FormaPrzedmiotu> FormyPrzedmiotu { get; set; }
        public DbSet<FormaZal> FormyZal { get; set; }
        public DbSet<TypPrzedmiotu> TypyPrzedmiotu { get; set; }
        public DbSet<FormaStudiow> FormyStudiow { get; set; }
        public DbSet<JezykStudiow> JezykiStudiow { get; set; }
        public DbSet<RodzajStudiow> RodzajeStudiow { get; set; }
        public DbSet<Poziom> Poziomy { get; set; }
        public DbSet<Obszar> Obszary { get; set; }
        public DbSet<Profil> Profile { get; set; }
        public DbSet<MEK> MEKI { get; set; }
        public DbSet<KEK> KEKI { get; set; }
        public DbSet<Kierunek> Kierunki { get; set; }
        public DbSet<UdzialProcentowy> UdzialyProcentowe { get; set; }
        public DbSet<Sladowanie> Sladowania { get; set; }
        public DbSet<ProgramKsztalcenia> ProgramyKsztalcenia { get; set; }
        public DbSet<ProgramStudiow> ProgramyStudiow { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<KEKPrzedmiotu> KEKIPrzedmiotow { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Kurs> Kursy { get; set; }
        public DbSet<GrupaKursow> GrupyKursow { get; set; }
    }
}