using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class ItemLocations
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string ItemLocation { get; set; }

        /// <summary>
        /// Relationship with Items
        /// </summary>
        public ICollection<Items> Item { get; set; }
    }
}
