using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class SoldItems
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        /// <summary>
        /// Relationship with Customers.
        /// </summary>
        public int? CustomerId { get; set; }
        public Customers? Customer { get; set; }

        /// <summary>
        /// Relationship with Items.
        /// </summary>
        public int? SellerId { get; set; }
        public Sellers? Seller { get; set; }
    }
}
