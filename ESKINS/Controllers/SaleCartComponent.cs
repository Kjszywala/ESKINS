using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class SaleCartComponent : ViewComponent
	{
		#region Variables

		IErrorLogsServices errorLogsServices;
		IItemsServices itemServices;
		public List<ItemsModels>? sellCartModel;

		#endregion

		#region Constructor

		public SaleCartComponent(IErrorLogsServices _errorLogsServices, IItemsServices _itemServices)
		{
			errorLogsServices = _errorLogsServices;
			sellCartModel = new List<ItemsModels>();
			itemServices = _itemServices;
		}

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				return View("SaleCartComponent", sellCartModel);
			}
			catch (Exception ex)
			{
				await errorLogsServices.Error(ex);
				return View("Error");
			}
		}

		#endregion
	}
}
