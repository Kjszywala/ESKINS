namespace ESKINS.DbServices.Models
{
    public class SellersModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int? UserId { get; set; }
        public UsersModels? Users { get; set; }
        public List<OrdersModels>? Order { get; set; }
        public ICollection<SoldItemsModels>? SoldItem { get; set; }
    }
}
