using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("FormyStudiow")]
    public class FormaStudiow
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdFormyStudiow { get; set; }

        [MaxLength(40)]
        public string NazwaFormyStudiow { get; set; }
    }
}