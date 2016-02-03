using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("FormyPrzedmiotu")]
    public class FormaPrzedmiotu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdFormyPrzedmiotu { get; set; }

        [MaxLength(40)]
        [Required]
        public string NazwaFormy { get; set; }
    }
}