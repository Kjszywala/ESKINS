using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class Qualities
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

        /// <summary>
        /// Relationship with Items.
        /// </summary>
        public ICollection<Items>? Items { get; set; }
    }
}
