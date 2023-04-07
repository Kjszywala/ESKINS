using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class PaymentMethodsController : Controller
    {
        #region Variables

        IPaymentMethodsServices paymentMethodsServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public PaymentMethodsController(
            IPaymentMethodsServices _paymentMethodsServices,
            IErrorLogsServices _errorLogsServices)
        {
            paymentMethodsServices = _paymentMethodsServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var model = await paymentMethodsServices.GetAllActivePaymentMethods();
                return View(model);
            }
            catch(Exception e)
            {
                await errorLogsServices.Error(e);
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethodsModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var file = Request.Form.Files.FirstOrDefault();
                    if (file != null && file.Length > 0)
                    {
                        using (var stream = file.OpenReadStream())
                        {
                            using (var binaryReader = new BinaryReader(stream))
                            {
                                var imageData = binaryReader.ReadBytes((int)file.Length);
                                model.Image = imageData;
                            }
                        }
                    }
                    var IsConfirmed = await paymentMethodsServices.AddAsync(model);
                    if (IsConfirmed)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var model = await paymentMethodsServices.RemoveAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return RedirectToAction("Index");
            }
        }

        #endregion

    }
}
