using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models
{
    public class Users
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
        public string FirsName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }

        public decimal AccountBalance { get; set; }
    }
}
