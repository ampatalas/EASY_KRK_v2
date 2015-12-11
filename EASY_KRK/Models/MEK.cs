using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("MEKI")]
    public class MEK
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdMEK { get; set; }

        [MaxLength(40)]
        public string Kod { get; set; }

        [MaxLength(400)]
        public string Opis { get; set; }

        public int IdObszaru { get; set; }

        [ForeignKey("IdObszaru")]
        public virtual Obszar Obszar{ get; set; }

        public int IdPoziomu { get; set; }

        [ForeignKey("IdPoziomu")]
        public virtual Poziom Poziom { get; set; }

        public int IdProfilu { get; set; }

        [ForeignKey("IdProfilu")]
        public virtual Profil Profil { get; set; }

    }
}