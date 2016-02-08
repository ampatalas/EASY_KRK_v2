using EASY_KRK.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Models
{
    public class PrzypiszKEKViewModel : IValidatableObject
    {
        public SelectList KEKI { get; set; }
        public int SelectedKEK { get; set; }
        public int IdPrzedmiotu { get; set; }
        public string NazwaPrzedmiotu { get; set; }
        public string Filtr { get; set; }
        public bool Edycja { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            EASYKRKContext db = new EASYKRKContext();

            if (SelectedKEK != 0 && db.KEKIPrzedmiotow.Any(kekp => kekp.IdKEK == this.SelectedKEK 
                && kekp.IdPrzedmiotu == this.IdPrzedmiotu))
            {
                yield return new ValidationResult("Do przedmiotu został już przypisany ten KEK.", new[] { "SelectedKEK" });
            }
        }
    }
}