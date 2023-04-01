using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models
{
    public class Sellers
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
        /// Foreign key for user.
        /// </summary>
        public int UserId { get; set; }

        public Users Users { get; set; }
    }
}
