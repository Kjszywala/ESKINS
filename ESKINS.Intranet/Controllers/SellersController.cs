﻿using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class SellersController : Controller
    {
        #region Variables

        ISellersServices sellerServices;
        IUsersServices usersServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public SellersController(
            ISellersServices _sellerServices,
            IErrorLogsServices _errorLogsServices,
            IUsersServices _usersServices)
        {
            sellerServices = _sellerServices;
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
                var model = await sellerServices.GetAllAsync();
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
        public async Task<IActionResult> GetSellerAsync(int id)
        {
            var sellerMethod = await sellerServices.GetAsync(id);

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
        public async Task<IActionResult> Create(Sellers model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await sellerServices.AddAsync(model);
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
        public async Task<IActionResult> EditAsync(int id, Sellers model)
        {
            try
            {
                var oldModel = await sellerServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await sellerServices.EditAsync(id, model);
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
                var IsConfirmed = await sellerServices.RemoveAsync(id);
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
