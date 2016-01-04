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
        public Kurs KursWyklad { get; set; }
        public Kurs KursCwiczenia { get; set; }
        public Kurs KursLaboratorium { get; set; }
        public Kurs KursProjekt { get; set; }
        public Kurs KursSeminarium { get; set; }
        public Boolean GrupaWyklad { get; set; }
        public Boolean GrupaCwiczenia { get; set; }
        public Boolean GrupaLaboratorium { get; set; }
        public Boolean GrupaProjekt { get; set; }
        public Boolean GrupaSeminarium { get; set; }

    }
}