using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
    public class Items
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public string ProductName { get; set; }

        public string SerialNumber { get; set; }

        /// <summary>
        /// Relationship with Users.
        /// </summary>
        public int? UserId { get; set; }
        public Users? User { get; set; }

        /// <summary>
        /// Relationship with Categories.
        /// </summary>
        public int? CategoryId { get; set; }
        public Categories? Category { get; set; }

        /// <summary>
        /// Relationship with ItemLogs.
        /// </summary>
        public int? ItemLogsId { get; set; }
        public ItemLogs? ItemLog { get; set; }

        /// <summary>
        /// Relationship with ItemLocations.
        /// </summary>
        public int? ItemLocationId { get; set; }
        public ItemLocations? ItemLocation { get; set; }

        public bool StatTrack { get; set; }

        public bool Souvenir { get; set; }

        /// <summary>
        /// Relationship with ItemCollections.
        /// </summary>
        public int? ItemCollectionId { get; set; }
        public ItemCollections? ItemCollection { get; set; }

        /// <summary>
        /// Relationship with Phases.
        /// </summary>
        public int? PhaseId { get; set; }
        public Phases? Phase { get; set; }

        public string ItemFloat { get; set; }

        /// <summary>
        /// Relationship with Qualities.
        /// </summary>
        public int? QualityId { get; set; }
        public Qualities? Quality { get; set; }

        /// <summary>
        /// Relationship with Exteriors.
        /// </summary>
        public int? ExteriorId { get; set; }
        public Exteriors? Exterior { get; set; }

        public int Pattern { get; set; }

        public decimal ActualPrice { get; set; }

        public bool OnSale { get; set; }

        public decimal Discount { get; set; }

        public byte[] ItemImage { get; set; }

        /// <summary>
        /// Relationship to ItemPriceHistories.
        /// </summary>
        public ICollection<ItemPriceHistories>? ItemPriceHistory { get; set; }

        /// <summary>
        /// Relationship to Items.
        /// </summary>
        public ICollection<Orders>? Orders { get; set; }

        /// <summary>
        /// Relationship to Items.
        /// </summary>
        public ICollection<Targets>? Targets { get; set; }
    }
}
