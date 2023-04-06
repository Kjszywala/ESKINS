namespace ESKINS.DbServices.Models
{
    public class ItemsModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string ProductName { get; set; }
        public string SerialNumber { get; set; }
        public int? UserId { get; set; }
        public UsersModels? User { get; set; }
        public int? CategoryId { get; set; }
        public CategoriesModels? Category { get; set; }
        public int? ItemLogsId { get; set; }
        public ItemLogsModels? ItemLog { get; set; }
        public int? ItemLocationId { get; set; }
        public ItemLocationsModels? ItemLocation { get; set; }
        public bool StatTrack { get; set; }
        public bool Souvenir { get; set; }
        public int? ItemCollectionId { get; set; }
        public ItemCollectionsModels? ItemCollection { get; set; }
        public int? PhaseId { get; set; }
        public PhasesModels? Phase { get; set; }
        public string ItemFloat { get; set; }
        public int? QualityId { get; set; }
        public QualitiesModels? Quality { get; set; }
        public int? ExteriorId { get; set; }
        public ExteriorsModels? Exterior { get; set; }
        public int Pattern { get; set; }
        public decimal ActualPrice { get; set; }
        public bool OnSale { get; set; }
        public decimal Discount { get; set; }
        public byte[] ItemImage { get; set; }
        public ICollection<ItemPriceHistoriesModels>? ItemPriceHistorys { get; set; }
    }
}
