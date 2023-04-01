using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        [Display(Name = "Category Description")]
        public string? CategoryDescription { get; set; }

        /// <summary>
        /// Relationship with Items.
        /// </summary>
        public List<Items> Item { get; set; }
    }
}
