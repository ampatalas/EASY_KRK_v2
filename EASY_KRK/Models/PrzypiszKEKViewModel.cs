using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Models
{
    public class PrzypiszKEKViewModel
    {
        public SelectList KEKI { get; set; }
        public int SelectedKEK { get; set; }
        public int IdPrzedmiotu { get; set; }
        public string NazwaPrzedmiotu { get; set; }
        public string Filtr { get; set; }
        public bool Edycja { get; set; }
    }
}