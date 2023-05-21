using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class PhasesComponent : ViewComponent
	{
		#region Variables

		IPhasesServices phasesServices;
		IErrorLogsServices errorLogsServices;

		#endregion

		#region Constructor

		public PhasesComponent(IPhasesServices _phasesServices, IErrorLogsServices _errorLogsServices)
		{
			phasesServices = _phasesServices;
			errorLogsServices = _errorLogsServices;
		}

		#endregion

		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			try
			{
				return View("PhasesComponent", await phasesServices.GetAllAsync());
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
