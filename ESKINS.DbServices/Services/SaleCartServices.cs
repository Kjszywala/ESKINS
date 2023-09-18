using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
	public class SaleCartServices :
		BaseServices<SaleCart>,
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
