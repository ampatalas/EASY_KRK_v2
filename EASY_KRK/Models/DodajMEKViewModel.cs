using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Models
{
    public class DodajMEKViewModel
    {
        public SelectList MEKI { get; set; }
        public int SelectedMEK { get; set; }
        public int IdKEK { get; set; }
        public string Filtr { get; set; }
    }
}