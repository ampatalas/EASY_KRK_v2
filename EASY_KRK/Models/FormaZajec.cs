using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("FormyZajec")]
    public class FormaZajec
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdFormyZajec { get; set; }

        [MaxLength(40)]
        public string NazwaFormy { get; set; }
    }
}