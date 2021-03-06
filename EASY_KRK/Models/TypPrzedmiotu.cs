﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("TypyPrzedmiotu")]
    public class TypPrzedmiotu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdTypu { get; set; }

        [MaxLength(40)]
        [Required]
        public string NazwaTypu { get; set; }
    }
}