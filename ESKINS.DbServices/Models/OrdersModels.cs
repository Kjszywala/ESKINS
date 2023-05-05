namespace ESKINS.DbServices.Models
{
    public class OrdersModels 
    { 
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int? CustomerId { get; set; }
        public CustomersModels? Customer { get; set; }
        public int? SellerId { get; set; }
        public SellersModels? Seller { get; set; }
        public int? ItemId { get; set; }
        public ItemsModels? Item { get; set; }
        public decimal PricePaid { get; set; }
        public decimal Discount { get; set; }
        public ICollection<InvoicesModels>? Invoices { get; set; }
    
    }
}
