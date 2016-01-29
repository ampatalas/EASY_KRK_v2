using EASY_KRK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EASY_KRK.Controllers.Utils
{
    public class DeleteUtils
    {
        public static void UsunKategorieDb(EASYKRKContext db, int IdKategorii)
        {
            Kategoria Kategoria = db.Kategorie.Find(IdKategorii);
            List<Kategoria> Kategorie = Kategoria.Kategorie.ToList();
            List<Przedmiot> Przedmioty = Kategoria.Przedmioty.ToList();

            for (var i = 0; i < Przedmioty.Count; i++)
            {
                UsunPrzedmiotDb(db, Przedmioty[i].IdPrzedmiotu);
            }

            for (var i = 0; i < Kategorie.Count; i++)
            {
                UsunKategorieDb(db, Kategorie[i].IdKategorii);
            }

            db.Kategorie.Remove(db.Kategorie.Find(IdKategorii));
            db.SaveChanges();
        }

        public static void UsunPrzedmiotDb(EASYKRKContext db, int IdPrzedmiotu)
        {
            Przedmiot p = db.Przedmioty.Find(IdPrzedmiotu);
            List<Kurs> Kursy = p.Kursy.ToList();
            List<GrupaKursow> Grupy = p.GrupyKursow.ToList();
            List<KEKPrzedmiotu> KEKI = p.KEKI.ToList();

            for (var i = 0; i < Kursy.Count - 1; i++)
            {
                db.Kursy.Remove(Kursy[i]);
            }

            for (var j = 0; j < Grupy.Count - 1; j++)
            {
                db.GrupyKursow.Remove(Grupy[j]);
            }

            for (var k = 0; k < KEKI.Count - 1; k++)
            {
                db.KEKIPrzedmiotow.Remove(KEKI[k]);
            }

            db.Przedmioty.Remove(db.Przedmioty.Find(IdPrzedmiotu));

            db.SaveChanges();
        }
    }
}