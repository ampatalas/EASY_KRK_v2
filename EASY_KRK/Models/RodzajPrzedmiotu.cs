using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("RodzajePrzedmiotu")]
    public class RodzajPrzedmiotu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdRodzaju { get; set; }

        [MaxLength(40)]
        public string NazwaRodzaju { get; set; }
    }
}