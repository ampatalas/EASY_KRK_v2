﻿using EASY_KRK.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EASY_KRK.Models
{
    [Table("FormyZal")]
    public class FormaZal
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdFormyZal { get; set; }

        [MaxLength(40)]
        [Required]
        [Display(Name = "PassForm", ResourceType = typeof(Labels))]
        public string NazwaFormyZal { get; set; }
    }
}