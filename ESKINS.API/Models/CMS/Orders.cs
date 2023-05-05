using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class Orders
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

        public int? ItemId { get; set; }
        public Items? Item { get; set; }
        /// <summary>
        /// Relationship with Sellers.
        /// </summary>
        public int? SellerId { get; set; }
        public Sellers? Seller { get; set; }


        [Required(ErrorMessage = "Field is required")]
        public decimal PricePaid { get; set; }

        public decimal Discount { get; set; }

        /// <summary>
        /// Relationship to Invoices.
        /// </summary>
        public ICollection<Invoices>? Invoice { get; set; }
    }
}
