using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("Poziomy")]
    public class Poziom
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPoziomu { get; set; }

        [MaxLength(40)]
        [Required]
        public string NazwaPoziomu { get; set; }
    }
}