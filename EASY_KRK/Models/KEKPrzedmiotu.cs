using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("KEKIPrzedmiotow")]
    public class KEKPrzedmiotu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdKEKPrzedmiotu { get; set; }

        public int IdKEK { get; set; }

        [ForeignKey("IdKEK")]
        public virtual KEK KEK { get; set; }

        public int IdPrzedmiotu { get; set; }

        [ForeignKey("IdPrzedmiotu")]
        public virtual Przedmiot Przedmiot { get; set; }
    }
}