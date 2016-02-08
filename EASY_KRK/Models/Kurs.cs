using EASY_KRK.App_GlobalResources;
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
        [Display(Name = "CourseCode", ResourceType = typeof(Labels))]
        public string KodKursu { get; set; }

        [Display(Name = "CourseECTS", ResourceType = typeof(Labels))]
        public int PunktyECTS { get; set; }

        [Display(Name = "ZZU", ResourceType = typeof(Labels))]
        public int ZZU { get; set; }

        [Display(Name = "CNPS", ResourceType = typeof(Labels))]
        public int CNPS { get; set; }

        [Display(Name = "ECTSP", ResourceType = typeof(Labels))]
        public double ECTS_P { get; set; }

        [Display(Name = "ECTSBK", ResourceType = typeof(Labels))]
        public double ECTS_BK { get; set; }

        [Display(Name = "CoursePractical", ResourceType = typeof(Labels))]
        public bool Praktyczny { get; set; }

        [Display(Name = "CourseForm", ResourceType = typeof(Labels))]
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

        [NotMapped]
        public virtual bool CzyGrupa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CzyNalezyDoPrzedmiotu)
            {
                if (ZZU <= 0)
                {
                    yield return new ValidationResult(Labels.ZZUAmountError, new[] { "ZZU" });
                }

                if (CNPS <= 0)
                {
                    yield return new ValidationResult(Labels.CNPSAmountError, new[] { "CNPS" });
                }

                if (PunktyECTS <= 0)
                {
                    yield return new ValidationResult(Labels.ECTSAmountError, new[] { "PunktyECTS" });
                }

                if (ECTS_P < 0)
                {
                    yield return new ValidationResult(Labels.ECTSPAmountError, new[] { "ECTS_P" });
                }

                if (ECTS_P > PunktyECTS)
                {
                    yield return new ValidationResult(Labels.ECTSPECTSAmountError, new[] { "ECTS_P" });
                }

                if (ECTS_BK < 0)
                {
                    yield return new ValidationResult(Labels.ECTSBKAmountError, new[] { "ECTS_BK" });
                }

                if (ECTS_BK > PunktyECTS)
                {
                    yield return new ValidationResult(Labels.ECTSBKECTSAmountError, new[] { "ECTS_BK" });
                }
            }
        }
    }
}