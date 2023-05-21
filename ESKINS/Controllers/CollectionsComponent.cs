using ESKINS.BusinessLogic.BusinessLogic;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class CollectionsComponent : ViewComponent
	{
		#region Variables

		IItemCollectionsServices collectionServices;
		IErrorLogsServices errorLogsServices;

		#endregion

		#region Constructor

		public CollectionsComponent(IItemCollectionsServices _collectionServices, IErrorLogsServices _errorLogsServices)
		{
			collectionServices = _collectionServices;
			errorLogsServices = _errorLogsServices;
		}

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				return View("CollectionsComponent", await collectionServices.GetAllAsync());
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
