using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("Kierunki")]
    public class Kierunek
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKierunku { get; set; }

        [MaxLength(40)]
        [Required]
        public string NazwaKierunku { get; set; }

        public int IdRodzajuStudiow { get; set; }

        [ForeignKey("IdRodzajuStudiow")]
        public virtual RodzajStudiow RodzajStudiow { get; set; }

        public int IdPoziomu { get; set; }

        [ForeignKey("IdPoziomu")]
        public virtual Poziom Poziom { get; set; }

        public int IdProfilu { get; set; }

        [ForeignKey("IdProfilu")]
        public virtual Profil Profil { get; set; }
    }
}