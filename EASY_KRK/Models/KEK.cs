using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("KEKI")]
    public class KEK
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKEK { get; set; }

        [MaxLength(40)]
        public string Kod { get; set; }

        [MaxLength(150)]
        public string Opis { get; set; }

        public int IdKierunku { get; set; }

        [ForeignKey("IdKierunku")]
        public virtual Kierunek Kierunek { get; set; }
    }
}