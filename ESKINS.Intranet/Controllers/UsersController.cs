using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class UsersController : Controller
    {
        #region Variables
        IUsersServices usersServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public UsersController(
            IErrorLogsServices _errorLogsServices,
            IUsersServices _usersServices)
        {
            errorLogsServices = _errorLogsServices;
            usersServices = _usersServices;
        }

        #endregion

        #region Controllers

        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await usersServices.GetAllAsync();
                //foreach (var item in model)
                //{
                //    if (item != null)
                //    {
                //        item.Users = await usersServices.GetAsync(item.UserId.Value);
                //    }
                //}
                if (model == null)
                {
                    return View("Error");
                }
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var sellerMethod = await usersServices.GetAsync(id);

            if (sellerMethod == null)
            {
                return NotFound();
            }

            return Ok(sellerMethod);
        }

        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.Name = new SelectList(await usersServices.GetAllAsync(), "Id", "FirstName");
                return View();
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await usersServices.AddAsync(model);
                    if (IsConfirmed)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, UsersModels model)
        {
            try
            {
                var oldModel = await usersServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await usersServices.EditAsync(id, model);
                if (IsConfirmed)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        // GET: CategoriesController/Delete/5
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var IsConfirmed = await usersServices.RemoveAsync(id);
                if (IsConfirmed)
                {
                    return RedirectToAction("Index");
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
