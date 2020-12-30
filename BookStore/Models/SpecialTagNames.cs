using System.ComponentModel.DataAnnotations;

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