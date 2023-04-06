namespace ESKINS.DbServices.Models
{
    public class UsersAddressesModels
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string? HouseNumber { get; set; }
        public string? StreetName { get; set; }
        public string? PostCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public ICollection<CustomersModels> Customer { get; set; }
    }
}
