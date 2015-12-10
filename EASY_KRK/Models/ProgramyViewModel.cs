using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EASY_KRK.Models
{
    public class ProgramyViewModel
    {
        public IEnumerable<string> Kierunki;
        public IEnumerable<ProgramKsztalcenia> Programy;
        public int IdProgramu;
    }
}