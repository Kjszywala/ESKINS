using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models
{
    public class ItemPriceHistories
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
        public decimal Price { get; set; }
        public DateTime DateAndTime { get; set; }

        /// <summary>
        /// Relationship with Items.
        /// </summary>
        public int ItemId { get; set; }
        public Items Item { get; set; }
    }
}
