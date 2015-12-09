using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("Sladowania")]
    public class Sladowanie
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdSladowania { get; set; }

        public int IdMEK { get; set; }

        [ForeignKey("IdMEK")]
        public virtual MEK MEK { get; set; }

        public int IdKEK { get; set; }

        [ForeignKey("IdKEK")]
        public virtual KEK KEK { get; set; }
    }
}