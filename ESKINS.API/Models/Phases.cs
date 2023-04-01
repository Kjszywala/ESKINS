using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ESKINS.API.Models
{
    public class Phases
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
        public string Quality { get; set; }
    }
}
