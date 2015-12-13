using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    public class MacierzSladowaniaViewModel
    {
        public IEnumerable<KEK> KEKI { get; set; }
        public IEnumerable<Sladowanie> Sladowania { get; set; }
        public IEnumerable<UdzialProcentowy> Udzialy { get; set; }
        public IEnumerable<KEKPrzedmiotu> KEKIPrzedmiotow { get; set; }
        public Dictionary<int, int> Pokrycia { get; set; }
        public Dictionary<int, IEnumerable<MEK>> Nieprzypisane { get; set; }

    }
}