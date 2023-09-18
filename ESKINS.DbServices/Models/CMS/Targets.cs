using System.ComponentModel.DataAnnotations;

namespace ESKINS.DbServices.Models.CMS
{
    public class Targets
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
        /// Relationship with Users.
        /// </summary>
        public int? UserId { get; set; }
        public Users? User { get; set; }

        public decimal PriceUnder { get; set; }

        /// <summary>
        /// Relationship with Items.
        /// </summary>
        public int? ItemId { get; set; }
        public Items? Item { get; set; }

        /// <summary>
        /// Relationship with Phases.
        /// </summary>
        public int? PhaseId { get; set; }
        public Phases? Phase { get; set; }

        public string UnderFloat { get; set; }
    }
}
