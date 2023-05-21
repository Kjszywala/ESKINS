using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class CategoriesComponent : ViewComponent
	{
		#region Variables

		ICategoriesServices categoriesServices;
		IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public CategoriesComponent(ICategoriesServices _categoriesServices, IErrorLogsServices _errorLogsServices)
        {
            categoriesServices = _categoriesServices;
			errorLogsServices = _errorLogsServices;
        }

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				return View("CategoriesComponent", await categoriesServices.GetAllAsync());
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
