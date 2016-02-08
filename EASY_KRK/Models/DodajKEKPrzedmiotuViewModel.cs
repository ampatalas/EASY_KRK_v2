using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Models
{
    public class DodajKEKPrzedmiotuViewModel : IValidatableObject
    {
        public SelectList Przedmioty { get; set; }
        public int IdPrzedmiotu { get; set; }
        public int IdKEK { get; set; }
        public string Filtr { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            EASYKRKContext db = new EASYKRKContext();

            if (IdPrzedmiotu != 0 && db.KEKIPrzedmiotow.Any(kekp => kekp.IdKEK == this.IdKEK
                && kekp.IdPrzedmiotu == this.IdPrzedmiotu))
            {
                yield return new ValidationResult("Do przedmiotu został już przypisany ten KEK.", new[] { "IdPrzedmiotu" });
            }
        }
    }
}