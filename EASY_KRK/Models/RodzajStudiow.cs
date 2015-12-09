using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("RodzajeStudiow")]
    public class RodzajStudiow
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdRodzajuStudiow { get; set; }

        [MaxLength(40)]
        public string NazwaRodzajuStudiow { get; set; }
    }
}