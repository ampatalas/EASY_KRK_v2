using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("Kursy")]
    public class Kurs
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKursu { get; set; }

        [MaxLength(16)]
        public string KodKursu { get; set; }

        public int PunktyECTS { get; set; }

        public int ZZU { get; set; }

        public int CNPS { get; set; }

        public int ECTS_P { get; set; }

        public int ECTS_BK { get; set; }

        public bool Praktyczny { get; set; }

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