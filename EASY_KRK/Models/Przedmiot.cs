using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("Przedmioty")]
    public class Przedmiot
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPrzedmiotu { get; set; }

        [MaxLength(15)]
        [Display(Name = "Kod przedmiotu: ")]
        public string KodPrzedmiotu { get; set; }

        [Display(Name = "Nazwa przedmiotu: ")]
        [MaxLength(100)]
        public string NazwaPrzedmiotu { get; set; }

        [Display(Name = "Rodzaj: ")]
        public int IdRodzajuPrzedmiotu { get; set; }

        [ForeignKey("IdRodzajuPrzedmiotu")]
        public virtual RodzajPrzedmiotu RodzajPrzedmiotu { get; set; }

        [Display(Name = "Forma: ")]
        public int IdFormyPrzedmiotu { get; set; }

        [ForeignKey("IdFormyPrzedmiotu")]
        public virtual FormaPrzedmiotu FormaPrzedmiotu { get; set; }

        [Display(Name = "Typ: ")]
        public int IdTypuPrzedmiotu { get; set; }

        [ForeignKey("IdTypuPrzedmiotu")]
        public virtual TypPrzedmiotu TypPrzedmiotu { get; set; }

        public int IdKategorii { get; set; }

        [ForeignKey("IdKategorii")]
        public virtual Kategoria Kategoria { get; set; }

        [Display(Name = "Ogólnouczelniany? ")]
        public bool Ogolnouczelniany { get; set; }

        public virtual ICollection<Kurs> Kursy { get; set; }
        public virtual ICollection<GrupaKursow> GrupyKursow { get; set; }

    }
}