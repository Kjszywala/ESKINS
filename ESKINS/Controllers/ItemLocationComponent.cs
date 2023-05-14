using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class ItemLocationComponent : ViewComponent
	{
		#region Variables

		IItemLocationsServices itemLocationServices;
		IErrorLogsServices errorLogsServices;

		#endregion

		#region Constructor

		public ItemLocationComponent(IItemLocationsServices _itemLocationServicess, IErrorLogsServices _errorLogsServices)
		{
			itemLocationServices = _itemLocationServicess;
			errorLogsServices = _errorLogsServices;
		}

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				return View("ItemLocationComponent", await itemLocationServices.GetAllAsync());
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
