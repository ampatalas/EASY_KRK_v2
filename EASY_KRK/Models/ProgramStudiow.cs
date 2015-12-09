using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("ProgramyStudiow")]
    public class ProgramStudiow
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdProgramuStudiow { get; set; }

        public int IdProgramuKsztalcenia { get; set; }

        [ForeignKey("IdProgramuKsztalcenia")]
        public virtual ProgramKsztalcenia ProgramKsztalcenia { get; set; }
    }

}