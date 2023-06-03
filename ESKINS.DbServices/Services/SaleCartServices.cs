using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
	public class SaleCartServices :
		BaseServices<SaleCartModels>,
		ISaleCartServices
	{
		#region Constructor

		public SaleCartServices()
			: base("/api/v1.0/SaleCarts/")
		{
		}

		#endregion
	}
}
