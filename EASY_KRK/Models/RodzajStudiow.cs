using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("RodzajeStudiow")]
    public class RodzajStudiow
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdRodzajuStudiow { get; set; }

        [MaxLength(40)]
        [Required]
        public string NazwaRodzajuStudiow { get; set; }
    }
}