using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class Customers
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string LastName { get; set; }

        /// <summary>
        /// Relationship with Users.
        /// </summary>
        public int UserId { get; set; }
        public Users User { get; set; }

        /// <summary>
        /// Relationship with UsersAddresses.
        /// </summary>
        public int CustomerAddressId { get; set; }
        public UsersAddresses CustomerAddress { get; set; }

        /// <summary>
        /// Relationship with Orders.
        /// </summary>
        public List<Orders> Order { get; set; }

        /// <summary>
        /// Relationship with SoldItems.
        /// </summary>
        public List<SoldItems> SoldItem { get; set; }

    }
}
