using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("FormyPrzedmiotu")]
    public class FormaPrzedmiotu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdFormyPrzedmiotu { get; set; }

        [MaxLength(40)]
        public string NazwaFormy { get; set; }
    }
}