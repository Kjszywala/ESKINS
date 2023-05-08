using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class InvoicesController : Controller
    {
        #region Variables

        IInvoicesServices invoicesServices;
        IErrorLogsServices errorLogsServices;
        IOrdersServices ordersServices;
        IPaymentMethodsServices paymentMethodsServices;

        #endregion

        #region Constructor

        public InvoicesController(
            IInvoicesServices _invoicesServices,
            IErrorLogsServices _errorLogsServices,
            IOrdersServices _ordersServices,
            IPaymentMethodsServices _paymentMethodsServices)
        {
            invoicesServices = _invoicesServices;
            errorLogsServices = _errorLogsServices;
            ordersServices = _ordersServices;
            paymentMethodsServices = _paymentMethodsServices;
        }

        #endregion

        #region Controllers

        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await invoicesServices.GetAllAsync();
                foreach (var invoice in model)
                {
                    invoice.Orders = await ordersServices.GetAsync(invoice.OrderId.Value);
                    invoice.PaymentMethods = await paymentMethodsServices.GetAsync(invoice.PaymentMethodId.Value);
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
        public async Task<IActionResult> GetInvoiceAsync(int id)
        {
            var phasesMethod = await invoicesServices.GetAsync(id);

            if (phasesMethod == null)
            {
                return NotFound();
            }
            return Ok(phasesMethod);
        }

        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.Order = new SelectList(await ordersServices.GetAllAsync(), "Id", "Title");
                ViewBag.Payment = new SelectList(await paymentMethodsServices.GetAllAsync(), "Id", "Title");
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
        public async Task<IActionResult> Create(InvoicesModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await invoicesServices.AddAsync(model);
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
        public async Task<IActionResult> EditAsync(int id, InvoicesModels model)
        {
            try
            {
                var oldModel = await invoicesServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await invoicesServices.EditAsync(id, model);
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
                var IsConfirmed = await invoicesServices.RemoveAsync(id);
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
