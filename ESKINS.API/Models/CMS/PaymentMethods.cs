using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class PaymentMethods
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public string? ImageName { get; set; }

        public byte[]? Image { get; set; }

        /// <summary>
        /// Relationship to Invoices.
        /// </summary>
        public ICollection<Invoices> Invoice { get; set; }
    }
}
