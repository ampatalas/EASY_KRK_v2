using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("Kursy")]
    public class Kurs
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
        public float ECTS_P { get; set; }

        [Display(Name = "Punkty ECTS (BK): ")]
        public float ECTS_BK { get; set; }

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
    }
}