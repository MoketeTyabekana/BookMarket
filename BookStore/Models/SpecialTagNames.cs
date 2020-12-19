using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SpecialTagNames
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Special Tag")]

        public string SpecialTagName { get; set; }
    }
}

