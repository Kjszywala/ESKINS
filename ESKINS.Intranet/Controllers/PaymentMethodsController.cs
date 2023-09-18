using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;
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
                if (model == null)
                {
                    return View("Error");
                }
                return View(model);
            }
            catch(Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethods model)
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
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMethodAsync(int id)
        {
            var paymentMethod = await paymentMethodsServices.GetAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethodModel = new PaymentMethods
            {
                Id = paymentMethod.Id,
                Title = paymentMethod.Title,
                IsActive = paymentMethod.IsActive,
                ImageName = paymentMethod.ImageName,
                Image = paymentMethod.Image
            };

            return Ok(paymentMethodModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var IsConfirmed = await paymentMethodsServices.RemoveAsync(id);
                if(IsConfirmed)
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

        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, PaymentMethods model)
        {
            try
            {
                id = model.Id;
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
                else 
                {
                    var oldModel = await paymentMethodsServices.GetAsync(id);
                    model.Image = oldModel.Image; 
                }
                var IsConfirmed = await paymentMethodsServices.EditAsync(id,model);
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
