using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class Invoices
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Relationship with Orders.
        /// </summary>
        public int OrderId { get; set; }
        public Orders Order { get; set; }

        /// <summary>
        /// Relationship with PaymentMethods.
        /// </summary>
        public int PaymentMethodId { get; set; }
        public PaymentMethods PaymentMethod { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
