using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class BuyCartController : Controller
    {
        #region Properties

        IItemsServices itemsServices;
        IErrorLogsServices errorLogsServices;
        ICategoriesServices categoriesServices;
        IExteriorsServices exteriorsServices;
        IQualitiesServices qualitiesServices;

        #endregion

        #region Constructor

        public BuyCartController(
            IItemsServices _itemsServices,
            IErrorLogsServices _errorLogsServices,
            ICategoriesServices _categoriesServices,
            IExteriorsServices _exteriorsServices,
            IQualitiesServices _qualitiesServices)
        {
            itemsServices = _itemsServices;
            errorLogsServices = _errorLogsServices;
            categoriesServices = _categoriesServices;
            exteriorsServices = _exteriorsServices;
            qualitiesServices = _qualitiesServices;

        }

        #endregion

        #region Methods

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var list = itemsServices.GetAllAsync().Result;
                foreach(var item in list) { 
                    item.Category = categoriesServices.GetAsync(item.CategoryId.Value).Result;
                    item.Exterior = exteriorsServices.GetAsync(item.ExteriorId.Value).Result;
                    item.Quality = qualitiesServices.GetAsync(item.QualityId.Value).Result;
                }
                return View(list);
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
