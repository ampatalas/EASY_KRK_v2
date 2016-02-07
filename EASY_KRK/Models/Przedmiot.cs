using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Linq;
using EASY_KRK.Controllers.Utils;
using EASY_KRK;
using EASY_KRK.App_GlobalResources;

namespace EASY_KRK.Models
{
    [Table("Przedmioty")]
    public class Przedmiot : IValidatableObject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPrzedmiotu { get; set; }

        [Display(Name = "SubjectCode", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = "CodeErrorEmpty")]
        [StringLength(15, MinimumLength = 3, ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = "CodeErrorLength")]
        [RegularExpression(@"^[A-Za-z\d]*$", ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = "CodeErrorExpression")]
        public string KodPrzedmiotu { get; set; }

        [Display(Name = "SubjectName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = "NameErrorEmpty")]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = "NameErrorLength")]
        [RegularExpression(@"^[A-Za-zĄĆĘŁŃÓŚŻŹąćęłńóśżź]+([ ]{1}[A-Za-zZĄĆĘŁŃÓŚŻŹa-ząćęłńóśżź\d]+)*$", ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = "NameErrorExpression")]
        public string NazwaPrzedmiotu { get; set; }

        [Display(Name = "SubjectKind", ResourceType = typeof(Labels))]
        public int IdRodzajuPrzedmiotu { get; set; }

        [ForeignKey("IdRodzajuPrzedmiotu")]
        public virtual RodzajPrzedmiotu RodzajPrzedmiotu { get; set; }

        [Display(Name = "SubjectForm", ResourceType = typeof(Labels))]
        public int IdFormyPrzedmiotu { get; set; }

        [ForeignKey("IdFormyPrzedmiotu")]
        public virtual FormaPrzedmiotu FormaPrzedmiotu { get; set; }

        [Display(Name = "SubjectType", ResourceType = typeof(Labels))]
        public int IdTypuPrzedmiotu { get; set; }

        [ForeignKey("IdTypuPrzedmiotu")]
        public virtual TypPrzedmiotu TypPrzedmiotu { get; set; }

        public int IdKategorii { get; set; }

        [ForeignKey("IdKategorii")]
        public virtual Kategoria Kategoria { get; set; }

        [Display(Name = "SubjectUni", ResourceType = typeof(Labels))]
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
                yield return new ValidationResult(Labels.CodeErrorLength, new[] { "KodPrzedmiotu" });
            }

            if (NazwaPrzedmiotu.Length < 3 || NazwaPrzedmiotu.Length > 100)
            {
                yield return new ValidationResult(Labels.NameErrorLength, new[] { "NazwaPrzedmiotu" });
            }

            if (!Regex.IsMatch(KodPrzedmiotu, KodWzor))
            {
                yield return new ValidationResult(Labels.CodeErrorExpression, new[] { "KodPrzedmiotu" });
            }

            if (!Regex.IsMatch(NazwaPrzedmiotu, NazwaWzor))
            {
                yield return new ValidationResult(Labels.NameErrorExpression, new[] { "NazwaPrzedmiotu" });
            }

            if (string.IsNullOrEmpty(KodPrzedmiotu))
            {
                yield return new ValidationResult(Labels.CodeErrorEmpty, new[] { "KodPrzedmiotu" });
            }

            if (string.IsNullOrEmpty(NazwaPrzedmiotu))
            {
                yield return new ValidationResult(Labels.NameErrorEmpty, new[] { "NazwaPrzedmiotu" });
            }

            if (db.Przedmioty.Any(p => p.KodPrzedmiotu == this.KodPrzedmiotu
                && Kat.IdProgramuStudiow == p.Kategoria.IdProgramuStudiow 
                && p.IdPrzedmiotu != this.IdPrzedmiotu))
            {
                
                yield return new ValidationResult(Labels.SubjectCodeExists, new[] { "KodPrzedmiotu" });
            }

            if (db.Przedmioty.Any(p => p.NazwaPrzedmiotu == this.NazwaPrzedmiotu
                && Kat.IdProgramuStudiow == p.Kategoria.IdProgramuStudiow 
                && p.IdPrzedmiotu != this.IdPrzedmiotu))
            {
                yield return new ValidationResult(Labels.SubjectNameExists, new[] { "NazwaPrzedmiotu" });
            }
        }

    }
}