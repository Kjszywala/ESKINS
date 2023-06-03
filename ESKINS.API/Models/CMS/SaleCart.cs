using System.ComponentModel.DataAnnotations;

namespace ESKINS.API.Models.CMS
{
	public class SaleCart
	{
		[Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string SessionId { get; set; }
        public decimal ItemActualPrice{ get; set; }
        public decimal ItemDiscount{ get; set; }
        public string ItemName{ get; set; }
        public byte[] ItemImage{ get; set; }
    }
}
