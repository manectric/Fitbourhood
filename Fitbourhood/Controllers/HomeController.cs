using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Dapper;
using Fitbourhood.Filter;
using Fitbourhood.Helpers;
using Fitbourhood.Models;
using Fitbourhood.Repositories;

namespace Fitbourhood.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            bool emailAndPasswordAreEmpty = string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password);
            UserContextModel userContextModel = UsersRepository.ValidatePasswordIsCorrect(email, password);
            if (userContextModel == null)
            {
                ViewBag.ErrorMessages = UsersRepository.ErrorList;
                return View("Index");
            }
            else
            {
                UserContextHelper.SetUserSession(userContextModel);
                return RedirectToAction("Index", "SportEvent");
            }
        }

        public ActionResult Logout()
        {
            UserContextHelper.SetUserSession(null);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            UserRegisterModel user = new UserRegisterModel();
            return View("Register", user);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserRegisterModel user)
        {
            if (UsersRepository.AddUser(user))
            {
                ModelState.Clear();
                ViewBag.SuccessMessages = "Konto zostało utworzone pomyślnie";
                return View("Index");
            }

            ViewBag.ErrorMessages = UsersRepository.ErrorList;
            return View("Register");
        }

        [AllowAnonymous]
        public ActionResult PasswordReminder()
        {
            return View("PasswordReminder");
        }

        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }
    }
}