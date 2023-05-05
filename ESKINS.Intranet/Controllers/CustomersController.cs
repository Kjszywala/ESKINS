using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class CustomersController : Controller
    {
        #region Variables

        ICustomersServices customersService;
        IErrorLogsServices errorLogsServices;
        IUsersServices usersServices;

        #endregion

        #region Constructor

        public CustomersController(
            ICustomersServices _customersService,
            IErrorLogsServices _errorLogsServices,
            IUsersServices _usersServices)
        {
            customersService = _customersService;
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
                var model = await customersService.GetAllAsync();
                foreach (var item in model)
                {
                    item.User = await usersServices.GetAsync(item.UserId);
                }
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
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var customersMethod = await customersService.GetAsync(id);

            if (customersMethod == null)
            {
                return NotFound();
            }

            return Ok(customersMethod);
        }

        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.Name = new SelectList(await usersServices.GetAllAsync(), "Id", "Email");
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
        public async Task<IActionResult> Create(CustomersModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                //if (ModelState.IsValid)
                //{
                model.User = await usersServices.GetAsync(model.Id);
                var IsConfirmed = await customersService.AddAsync(model);
                if (IsConfirmed)
                {
                    return RedirectToAction("Index");
                }
                //}
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
        public async Task<IActionResult> EditAsync(int id, CustomersModels model)
        {
            try
            {
                var oldModel = await customersService.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await customersService.EditAsync(id, model);
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
                var IsConfirmed = await customersService.RemoveAsync(id);
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

            #endregion

        }
    }
}
