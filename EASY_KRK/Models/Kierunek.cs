using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("Kierunki")]
    public class Kierunek
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKierunku { get; set; }

        [MaxLength(40)]
        public string NazwaKierunku { get; set; }

        public int IdRodzajuStudiow { get; set; }

        [ForeignKey("IdRodzajuStudiow")]
        public virtual RodzajStudiow RodzajStudiow { get; set; }
    }
}