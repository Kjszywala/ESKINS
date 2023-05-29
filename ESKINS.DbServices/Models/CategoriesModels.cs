using System.ComponentModel.DataAnnotations;

namespace ESKINS.DbServices.Models
{
    public class CategoriesModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        [Display(Name = "Category")]
        public string? CategoryDescription { get; set; }
        public ICollection<ItemsModels>? Items { get; set; }
    }
}
