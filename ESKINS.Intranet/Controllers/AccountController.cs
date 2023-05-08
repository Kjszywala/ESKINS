using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.Intranet.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class AccountController : Controller
    {
        #region Injection

        IUsersServices usersService;
        IErrorLogsServices errorLogsService;

        #endregion

        #region Constructor

        public AccountController(IErrorLogsServices _errorLogsServices, IUsersServices _usersServices)
        {
            usersService = _usersServices;
            errorLogsService = _errorLogsServices;
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
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index");
                }
                foreach (var item in users)
                {
                    if (email.Trim() == item.Email.Trim() && password.Trim() == item.Password.Trim())
                    {
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
                return View("Index");
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
                    return RedirectToAction("Index", "Home");
                }
                // display an error message
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View("Index");
            }
            catch(Exception ex)
            {
                await errorLogsService.Error(ex);
                return View("Index");
            }
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            // clear the session variable
            HttpContext.Session.Remove("username");

            // redirect the user to the login page
            return RedirectToAction("Login");
        }

        #endregion

    }
}
