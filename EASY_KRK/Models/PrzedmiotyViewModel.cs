using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    public class PrzedmiotyViewModel
    {
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public int IdPrzedmiotu { get; set; }
    }
}