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

        [Display(Name = "Kod przedmiotu: ")]
        [Required(ErrorMessage = "Kod przedmiotu nie może być pusty")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Kod przedmiotu musi zawierać od 3 do 15 znaków.")]
        [RegularExpression(@"^[A-Za-z\d]*$", ErrorMessage = "Kod przedmiotu może zawierać tylko znaki alfanumeryczne")]
        public string KodPrzedmiotu { get; set; }

        [Display(Name = "Nazwa przedmiotu: ")]
        [Required(ErrorMessage = "Nazwa przedmiotu nie może być pusta")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa przedmiotu musi zawierać od 3 do 100 znaków.")]
        [RegularExpression(@"^[A-Za-zĄĆĘŁŃÓŚŻŹąćęłńóśżź]+([ ]{1}[A-Za-zZĄĆĘŁŃÓŚŻŹa-ząćęłńóśżź\d]+)*$", ErrorMessage = "Nazwa przedmiotu może zawierać tylko cyfry lub litery alfabetu polskiego.")]
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
        public virtual ICollection<KEKPrzedmiotu> KEKI { get; set; }

    }
}