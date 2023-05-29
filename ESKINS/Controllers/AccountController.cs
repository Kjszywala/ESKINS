using ESKINS.BusinessLogic.BusinessLogic;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class AccountController : Controller
    {
        #region Injection

        IUsersServices usersService;
        IErrorLogsServices errorLogsService;
        ICartServices cartServices;

        #endregion

        #region Properties

        private readonly CardSessionLogic cardSessionLogic;

        #endregion

        #region Constructor

        public AccountController(
            IErrorLogsServices _errorLogsServices, 
            IUsersServices _usersServices,
            CardSessionLogic _cardSessionLogic,
            ICartServices _cartServices)
        {
            usersService = _usersServices;
            errorLogsService = _errorLogsServices;
            cardSessionLogic = _cardSessionLogic;
            cartServices = _cartServices;
        }

        #endregion

        #region Methods

        // GET: Account/Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var users = await usersService.GetAllAsync();
                // validate the username and password
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index");
                }
                foreach (var item in users)
                {
                    if (email.Trim() == item.Email.Trim() && password.Trim() == item.Password.Trim())
                    {
                        Config.SessionId = cardSessionLogic.GetCartSessionId();
                        Config.isConfirmed = true;
                        Config.UserId = item.Id;
                        Config.WalletAmount += item.AccountBalance;
                        // redirect the user to the protected page
                        return RedirectToAction("Index", "Home");
                    }
                }
                // display an error message
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View("Index");
            }
            catch (Exception ex)
            {
                await errorLogsService.Error(ex);
                return View("Error");
            }
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsersModels model)
        {
            try
            {
                model.IsActive = true;
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                model.AccountBalance = 0;
                // validate the username and password
                var confirmed = await usersService.AddAsync(model);
                if (confirmed)
                {
                    return RedirectToAction("Index", "Account");
                }
                // display an error message
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View("Index");
            }
            catch (Exception ex)
            {
                await errorLogsService.Error(ex);
                return View("Error");
            }
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            // clear the session variable
            Config.isConfirmed = false;
            Config.WalletAmount = 0;

            // redirect the user to the login page
            return RedirectToAction("Index");
        }

        #endregion

    }
}
