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
            catch(InvalidOperationException e)
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
                model.Invoices = null;
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
                //if (file != null && file.Length > 0)
                //{
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        file.CopyTo(memoryStream);
                //        model.Image = memoryStream.ToArray();
                //    }
                //}
                var model1 = new PaymentMethodsModels
                {
                    Title = model.Title,
                    IsActive = model.IsActive,
                    ImageName = model.ImageName,
                    Image = model.Image,
                    Invoices = new List<InvoicesModels>()
                };
                // Save model to database
                //foreach (var key in ModelState.Keys)
                //{
                //    foreach (var error in ModelState[key].Errors)
                //    {
                //        Console.WriteLine($"{key}: {error.ErrorMessage}");
                //    }
                //}
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await paymentMethodsServices.AddAsync(model1);
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

        #endregion

    }
}
