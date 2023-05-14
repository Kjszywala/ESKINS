using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class UniqueItemsComponent : ViewComponent
	{
		#region Variables

		IQualitiesServices qualityServices;
		IErrorLogsServices errorLogsServices;

		#endregion

		#region Constructor

		public UniqueItemsComponent(IQualitiesServices _qualityServices, IErrorLogsServices _errorLogsServices)
		{
			qualityServices = _qualityServices;
			errorLogsServices = _errorLogsServices;
		}

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				return View("UniqueItemsComponent", await qualityServices.GetAllAsync());
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
