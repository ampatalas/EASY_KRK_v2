using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    public class MacierzSladowaniaViewModel
    {
        public IEnumerable<KEK> KEKI;
        public IEnumerable<MEK> MEKI;
        public IEnumerable<Przedmiot> Przedmioty;
        public IEnumerable<Sladowanie> Sladowania;
        public IEnumerable<UdzialProcentowy> Udzialy;
        public IEnumerable<KEKPrzedmiotu> KEKIPrzedmiotow;

    }
}