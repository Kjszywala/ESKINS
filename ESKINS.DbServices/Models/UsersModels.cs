namespace ESKINS.DbServices.Models
{
    public class UsersModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal AccountBalance { get; set; }
        public ICollection<SellersModels>? Seller { get; set; }
        public ICollection<CustomersModels>? Customer { get; set; }
        public ICollection<ItemsModels>? Item { get; set; }
        public ICollection<TargetsModels>? Target { get; set; }
    }
}
