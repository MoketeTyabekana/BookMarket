using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Books
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Edition { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Publish Year")]
        public string YearPublished { get; set; }
       
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }

        [Required]
        [Display(Name = "Special Tag")]
        public int SpecialTagId { get; set; }

        [ForeignKey("SpecialTagId")]
        public SpecialTagNames SpecialTagName { get; set; }
    }
}