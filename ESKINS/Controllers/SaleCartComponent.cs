using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class SaleCartComponent : ViewComponent
	{
		#region Variables

		ISaleCartServices saleCartService;
		IErrorLogsServices errorLogsServices;
		public static List<SaleCartModels>? itemsModels;

		#endregion

		#region Constructor

		public SaleCartComponent(
			ISaleCartLogic _saleCartLogic,
			ISaleCartServices _saleCartService,
			IErrorLogsServices _errorLogsServices)
		{
			saleCartService = _saleCartService;
			errorLogsServices = _errorLogsServices;
		}

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				itemsModels = saleCartService.GetAllAsync().Result.Where(item => item.SessionId == Config.SessionId).ToList();
				return View("SaleCartComponent", itemsModels);
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
