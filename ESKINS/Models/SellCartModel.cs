using ESKINS.DbServices.Models;

namespace ESKINS.Models
{
    /// <summary>
    /// Cart for instand sale.
    /// </summary>
	public class SellCartModel
	{
        public decimal CurrentAmount { get; set; }
        public List<ItemsModels>? ItemsForInstantSaleList { get; set; }

        public SellCartModel()
        {
            CurrentAmount = 0;
            ItemsForInstantSaleList = new List<ItemsModels>();
        }
    }
}
