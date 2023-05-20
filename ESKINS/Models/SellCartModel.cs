using ESKINS.DbServices.Models;

namespace ESKINS.Models
{
    /// <summary>
    /// Cart for instand sale.
    /// </summary>
	public class SellCartModel
	{
        public static decimal CurrentAmount { get; set; }
        public static List<ItemsModels>? ItemsForInstantSaleList { get; set; }

        public SellCartModel()
        {
            CurrentAmount = 0;
        }
    }
}
