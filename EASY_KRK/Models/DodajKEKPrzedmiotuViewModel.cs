using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Models
{
    public class DodajKEKPrzedmiotuViewModel
    {
        public SelectList Przedmioty { get; set; }
        public int IdPrzedmiotu { get; set; }
        public int IdKEK { get; set; }
        public string Filtr { get; set; }
    }
}