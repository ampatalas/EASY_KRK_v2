using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PSI2015.Models
{
    [Table("ProgramyKsztalcenia")]
    public class ProgramKsztalcenia
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdProgramuKsztalcenia { get; set; }

        public int IdKierunku { get; set; }

        [ForeignKey("IdKierunku")]
        public virtual Kierunek Kierunek { get; set; }

        public int IdFormyStudiow { get; set; }

        [ForeignKey("IdFormyStudiow")]
        public virtual FormaStudiow FormaStudiow { get; set; }

        public int IdJezykaStudiow { get; set; }

        [ForeignKey("IdJezykaStudiow")]
        public virtual JezykStudiow JezykStudiow { get; set; }
    }
}