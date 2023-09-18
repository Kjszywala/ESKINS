using System.ComponentModel.DataAnnotations;

namespace ESKINS.DbServices.Models.CMS
{
    public class ItemLogs
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        [Display(Name = "Item Description")]
        public string? ItemLogDescription { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public decimal OldPrice { get; set; }

        public decimal? NewPrice { get; set; }
    }
}
