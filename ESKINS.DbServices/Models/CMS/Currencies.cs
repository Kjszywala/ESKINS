using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ESKINS.DbServices.Models.CMS
{
    public class Currencies
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        [Display(Name = "Category Description")]
        public string? CurrencyName { get; set; }

        public decimal? CurrentPrice { get; set; }
    }
}
