using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Models
{
    public class DodajPrzedmiotViewModel
    {
        public SelectList Kategorie { get; set; }
        public SelectList FormyPrzedmiotu { get; set; }
        public SelectList Rodzaje { get; set; }
        public SelectList Typy { get; set; }
        public SelectList FormyZaliczenia { get; set; }
        public Przedmiot Przedmiot { get; set; }
        public List<Kurs> Kursy { get; set; }
        public List<Boolean> CzyGrupa { get; set; }
        public List<KEK> KEKI { get; set; }

    }
}