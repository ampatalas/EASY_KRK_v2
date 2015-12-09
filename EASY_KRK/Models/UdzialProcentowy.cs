using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("UdzialyProcentowe")]
    public class UdzialProcentowy
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUdzialu { get; set; }

        public double Wartosc { get; set; }

        public int IdKierunku { get; set; }

        [ForeignKey("IdKierunku")]
        public virtual Kierunek Kierunek { get; set; }

        public int IdObszaru { get; set; }

        [ForeignKey("IdObszaru")]
        public virtual Obszar Obszar { get; set; }
    }
}