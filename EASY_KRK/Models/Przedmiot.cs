using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Linq;
using EASY_KRK.Controllers.Utils;
using EASY_KRK;

namespace EASY_KRK.Models
{
    [Table("Przedmioty")]
    public class Przedmiot : IValidatableObject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPrzedmiotu { get; set; }

        //[Display(Name = "Kod przedmiotu: ")]
        [Display(Name = "SubjectCode", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessage = "Kod przedmiotu nie może być pusty")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Kod przedmiotu musi zawierać od 3 do 15 znaków.")]
        [RegularExpression(@"^[A-Za-z\d]*$", ErrorMessage = "Kod przedmiotu może zawierać tylko znaki alfanumeryczne")]
        public string KodPrzedmiotu { get; set; }

        [Display(Name = "Nazwa przedmiotu: ")]
        [Required(ErrorMessage = "Nazwa przedmiotu nie może być pusta")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa przedmiotu musi zawierać od 3 do 100 znaków.")]
        [RegularExpression(@"^[A-Za-zĄĆĘŁŃÓŚŻŹąćęłńóśżź]+([ ]{1}[A-Za-zZĄĆĘŁŃÓŚŻŹa-ząćęłńóśżź\d]+)*$", ErrorMessage = "Nazwa przedmiotu może zawierać tylko cyfry lub litery alfabetu polskiego.")]
        public string NazwaPrzedmiotu { get; set; }

        [Display(Name = "Rodzaj: ")]
        public int IdRodzajuPrzedmiotu { get; set; }

        [ForeignKey("IdRodzajuPrzedmiotu")]
        public virtual RodzajPrzedmiotu RodzajPrzedmiotu { get; set; }

        [Display(Name = "Forma: ")]
        public int IdFormyPrzedmiotu { get; set; }

        [ForeignKey("IdFormyPrzedmiotu")]
        public virtual FormaPrzedmiotu FormaPrzedmiotu { get; set; }

        [Display(Name = "Typ: ")]
        public int IdTypuPrzedmiotu { get; set; }

        [ForeignKey("IdTypuPrzedmiotu")]
        public virtual TypPrzedmiotu TypPrzedmiotu { get; set; }

        public int IdKategorii { get; set; }

        [ForeignKey("IdKategorii")]
        public virtual Kategoria Kategoria { get; set; }

        [Display(Name = "Ogólnouczelniany? ")]
        public bool Ogolnouczelniany { get; set; }

        public virtual ICollection<Kurs> Kursy { get; set; }
        public virtual ICollection<GrupaKursow> GrupyKursow { get; set; }
        public virtual ICollection<KEKPrzedmiotu> KEKI { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            EASYKRKContext db = new EASYKRKContext();
            string KodWzor = @"^[A-Za-z\d]*$";
            string NazwaWzor = @"^[A-Za-zĄĆĘŁŃÓŚŻŹąćęłńóśżź]+([ ]{1}[A-Za-zZĄĆĘŁŃÓŚŻŹa-ząćęłńóśżź\d]+)*$";
            Kategoria Kat = db.Kategorie.Find(this.IdKategorii);

            if (KodPrzedmiotu.Length < 3 || KodPrzedmiotu.Length > 15)
            {
                yield return new ValidationResult("Kod przedmiotu musi zawierać od 3 do 15 znaków.", new[] { "KodPrzedmiotu" });
            }

            if (NazwaPrzedmiotu.Length < 3 || NazwaPrzedmiotu.Length > 100)
            {
                yield return new ValidationResult("Nazwa przedmiotu musi zawierać od 3 do 100 znaków.", new[] { "NazwaPrzedmiotu" });
            }

            if (!Regex.IsMatch(KodPrzedmiotu, KodWzor))
            {
                yield return new ValidationResult("Kod przedmiotu może zawierać tylko znaki alfanumeryczne.", new[] { "KodPrzedmiotu" });
            }

            if (!Regex.IsMatch(NazwaPrzedmiotu, NazwaWzor))
            {
                yield return new ValidationResult("Nazwa przedmiotu może zawierać tylko cyfry lub litery alfabetu polskiego.", new[] { "NazwaPrzedmiotu" });
            }

            if (string.IsNullOrEmpty(KodPrzedmiotu))
            {
                yield return new ValidationResult("Kod przedmiotu nie może być pusty.", new[] { "KodPrzedmiotu" });
            }

            if (string.IsNullOrEmpty(NazwaPrzedmiotu))
            {
                yield return new ValidationResult("Nazwa przedmiotu nie może być pusta.", new[] { "NazwaPrzedmiotu" });
            }

            if (db.Przedmioty.Any(p => p.KodPrzedmiotu == this.KodPrzedmiotu
                && Kat.IdProgramuStudiow == p.Kategoria.IdProgramuStudiow 
                && p.IdPrzedmiotu != this.IdPrzedmiotu))
            {
                
                yield return new ValidationResult("Przedmiot o podanym kodzie już istnieje.", new[] { "KodPrzedmiotu" });
            }

            if (db.Przedmioty.Any(p => p.NazwaPrzedmiotu == this.NazwaPrzedmiotu
                && Kat.IdProgramuStudiow == p.Kategoria.IdProgramuStudiow 
                && p.IdPrzedmiotu != this.IdPrzedmiotu))
            {
                yield return new ValidationResult("Przedmiot o podanej nazwie już istnieje.", new[] { "NazwaPrzedmiotu" });
            }
        }

    }
}