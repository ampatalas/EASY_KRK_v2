using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("Kursy")]
    public class Kurs : IValidatableObject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKursu { get; set; }

        [MaxLength(16)]
        [Display(Name = "Kod kursu:")]
        public string KodKursu { get; set; }

        [Display(Name = "Punkty ECTS:")]
        public int PunktyECTS { get; set; }

        [Display(Name = "ZZU: ")]
        public int ZZU { get; set; }

        [Display(Name = "CNPS: ")]
        public int CNPS { get; set; }

        [Display(Name = "Punkty ECTS (P): ")]
        public double ECTS_P { get; set; }

        [Display(Name = "Punkty ECTS (BK): ")]
        public double ECTS_BK { get; set; }

        [Display(Name = "Praktyczny? ")]
        public bool Praktyczny { get; set; }

        [Display(Name = "Forma zajęć: ")]
        public int IdFormyZajec { get; set; }

        [ForeignKey("IdFormyZajec")]
        public virtual FormaZajec FormaZajec { get; set; }

        public int? IdFormyZal { get; set; }

        [ForeignKey("IdFormyZal")]
        public virtual FormaZal FormaZal { get; set; }

        public int IdPrzedmiotu { get; set; }

        [ForeignKey("IdPrzedmiotu")]
        public virtual Przedmiot Przedmiot { get; set; }

        public int? IdGrupyKursow { get; set; }

        [ForeignKey("IdGrupyKursow")]
        public virtual GrupaKursow GrupaKursow { get; set; }

        [NotMapped]
        public virtual bool CzyNalezyDoPrzedmiotu { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CzyNalezyDoPrzedmiotu)
            {
                if (ZZU <= 0)
                {
                    yield return new ValidationResult("ZZU musi być większe od 0.", new[] { "ZZU" });
                }

                if (CNPS <= 0)
                {
                    yield return new ValidationResult("CNPS musi być większe od 0.", new[] { "CNPS" });
                }

                if (PunktyECTS <= 0)
                {
                    yield return new ValidationResult("Liczba punktów ECTS musi być większa od 0.", new[] { "PunktyECTS" });
                }

                if (ECTS_P < 0)
                {
                    yield return new ValidationResult("Liczba punktów ECTS_P musi być większa lub równa 0.", new[] { "ECTS_P" });
                }

                if (ECTS_P > PunktyECTS)
                {
                    yield return new ValidationResult("Liczba punktów ECTS_P nie może przekraczać całkowitej liczby ECTS.", new[] { "ECTS_P" });
                }

                if (ECTS_BK < 0)
                {
                    yield return new ValidationResult("Liczba punktów ECTS_BK musi być większa lub równa 0.", new[] { "ECTS_BK" });
                }

                if (ECTS_BK > PunktyECTS)
                {
                    yield return new ValidationResult("Liczba punktów ECTS_BK nie może przekraczać całkowitej liczby ECTS.", new[] { "ECTS_BK" });
                }
            }
        }
    }
}