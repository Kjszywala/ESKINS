using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class ProfileController : Controller
    {
        #region Variables

        IUsersServices usersServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public ProfileController(IUsersServices _usersServices, IErrorLogsServices _errorLogsServices)
        {
            usersServices = _usersServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            return View(usersServices.GetAsync(Config.UserId).Result);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, Users model)
        {
            try
            {
                var oldModel = await usersServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                model.AccountBalance = oldModel.AccountBalance;
                var IsConfirmed = await usersServices.EditAsync(id, model);
                if (IsConfirmed)
                {
                    ViewBag.Message = "Succesfully updated!";
                    return View("Index", model);
                }
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        #endregion
    }
}
