using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("Kategorie")]
    public class Kategoria
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKategorii { get; set; }

        [MaxLength(100)]
        public string NazwaKategorii { get; set; }

        public int? MinECTS { get; set; }

        public int IdProgramuStudiow { get; set; }

        [ForeignKey("IdProgramuStudiow")]
        public virtual ProgramStudiow ProgramStudiow { get; set; }

        public int? IdKategoriiNadrzednej { get; set; }

        [ForeignKey("IdKategoriiNadrzednej")]
        public virtual Kategoria KategoriaNadrzedna { get; set; }
    }
}