using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class BuyCartComponent : ViewComponent
    {
        #region Variables

        ICartServices cartServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public BuyCartComponent(ICartServices _cartServices, IErrorLogsServices _errorLogsServices)
        {
            cartServices = _cartServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var list = cartServices.GetAllAsync().Result.Where(item => item.SessionId == Config.SessionId).ToList();
                return View("BuyCartComponent", list);
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