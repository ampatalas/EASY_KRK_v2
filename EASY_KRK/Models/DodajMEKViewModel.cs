using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Models
{
    public class DodajMEKViewModel : IValidatableObject
    {
        public SelectList MEKI { get; set; }
        public int SelectedMEK { get; set; }
        public int IdKEK { get; set; }
        public string Filtr { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            EASYKRKContext db = new EASYKRKContext();

            if (SelectedMEK != 0 && db.Sladowania.Any(slad => slad.IdKEK == this.IdKEK
                    && this.SelectedMEK == slad.IdMEK))
            {
                yield return new ValidationResult("Do KEKa został już przypisany ten MEK.", new[] { "SelectedMEK" });
            }
        }
    }
}