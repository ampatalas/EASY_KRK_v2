using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("GrupyKursow")]
    public class GrupaKursow
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdGrupyKursow { get; set; }

        [MaxLength(20)]
        public string KodGrupyKursow { get; set; }

        public int PunktyECTS { get; set; }

        public int ZZU { get; set; }

        public int CNPS { get; set; }

        public int ECTS_P { get; set; }

        public int ECTS_BK { get; set; }

        public bool Praktyczny { get; set; }

        public int IdPrzedmiotu { get; set; }

        [ForeignKey("IdPrzedmiotu")]
        public virtual Przedmiot Przedmiot { get; set; }

    }
}