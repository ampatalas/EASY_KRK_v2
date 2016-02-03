using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("JezykiStudiow")]
    public class JezykStudiow
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdJezyka { get; set; }

        [MaxLength(40)]
        [Required]
        public string NazwaJezyka { get; set; }
    }
}