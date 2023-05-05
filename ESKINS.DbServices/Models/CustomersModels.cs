namespace ESKINS.DbServices.Models
{
    public class CustomersModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public UsersModels? User { get; set; }
        public ICollection<OrdersModels>? Orders { get; set; }
        public ICollection<SoldItemsModels>? SoldItems { get; set; }
    }
}
