using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("FormyZajec")]
    public class FormaZajec
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdFormyZajec { get; set; }

        [MaxLength(40)]
        [Required]
        public string NazwaFormy { get; set; }
    }
}