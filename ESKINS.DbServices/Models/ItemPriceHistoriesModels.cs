namespace ESKINS.DbServices.Models
{
    public class ItemPriceHistoriesModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAndTime { get; set; }
        public int ItemId { get; set; }
        public ItemsModels Item { get; set; }
    }
}
