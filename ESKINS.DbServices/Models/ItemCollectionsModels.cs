namespace ESKINS.DbServices.Models
{
    public class ItemCollectionsModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string ItemCollection { get; set; }
        public ICollection<ItemsModels> Items { get; set; }
    }
}
