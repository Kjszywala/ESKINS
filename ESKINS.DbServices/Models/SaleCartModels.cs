namespace ESKINS.DbServices.Models
{
	public class SaleCartModels
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public decimal ItemActualPrice { get; set; }
		public decimal ItemDiscount { get; set; }
		public string ItemName { get; set; }
		public byte[] ItemImage { get; set; }
	}
}

