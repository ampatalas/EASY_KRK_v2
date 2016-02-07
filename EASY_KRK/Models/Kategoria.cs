using EASY_KRK.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("Kategorie")]
    public class Kategoria
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKategorii { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Labels))]
        [MaxLength(100)]
        [Required]
        public string NazwaKategorii { get; set; }

        [Display(Name = "Minimalna ilość ECTS: ")]
        public int? MinECTS { get; set; }

        public int IdProgramuStudiow { get; set; }

        [ForeignKey("IdProgramuStudiow")]
        public virtual ProgramStudiow ProgramStudiow { get; set; }

        public int? IdKategoriiNadrzednej { get; set; }

        [Display(Name = "Zawiera przedmioty")]
        [NotMapped]
        public bool zawieraPrzedmioty { get; set; }

        [ForeignKey("IdKategoriiNadrzednej")]
        public virtual Kategoria KategoriaNadrzedna { get; set; }

        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        public virtual ICollection<Kategoria> Kategorie { get; set; }
    }
}