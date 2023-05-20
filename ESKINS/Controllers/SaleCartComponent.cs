using ESKINS.DbServices.Interfaces;
using ESKINS.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class SaleCartComponent : ViewComponent
	{
		#region Variables

		IErrorLogsServices errorLogsServices;

		#endregion

		#region Constructor

		public SaleCartComponent(IErrorLogsServices _errorLogsServices)
		{
			errorLogsServices = _errorLogsServices;
		}

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				SellCartModel sellCartModel = new SellCartModel();
				return View("SaleCartComponent", sellCartModel);
			}
			catch (Exception ex)
			{
				await errorLogsServices.Error(ex);
				return View("Index");
			}
		}
		#endregion
	}
}
