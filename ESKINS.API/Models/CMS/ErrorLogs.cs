using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class ErrorLogs
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Description { get; set; }
    }
}
