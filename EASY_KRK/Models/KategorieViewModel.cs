using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    public class KategorieViewModel
    {
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public int IdKategorii { get; set; }
    }
}