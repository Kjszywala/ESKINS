﻿using System.ComponentModel.DataAnnotations;

namespace ESKINS.DbServices.Models.CMS
{
    public class Users
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

        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }

        public decimal AccountBalance { get; set; }

        /// <summary>
        /// Relationship with Users.
        /// </summary>
        public ICollection<Sellers>? Seller { get; set; }

        /// <summary>
        /// Relationship with Customers.
        /// </summary>
        public ICollection<Customers>? Customer { get; set; }

        /// <summary>
        /// Relationship with Items.
        /// </summary>
        public ICollection<Items>? Item { get; set; }

        /// <summary>
        /// Relationship with Items.
        /// </summary>
        public ICollection<Targets>? Target { get; set; }
    }
}
