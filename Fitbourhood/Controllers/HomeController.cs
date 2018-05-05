using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using Fitbourhood.Models;
using Fitbourhood.Repositories;

namespace Fitbourhood.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Login");
        }


        public ActionResult Login(string email, string password)
        {
            bool emailAndPasswordAreEmpty = string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password);
            bool passwordIsCorrect = emailAndPasswordAreEmpty ? false : UsersRepository.ValidatePasswordIsCorrect(email, password);
            if (emailAndPasswordAreEmpty || !passwordIsCorrect)
            {
                ViewBag.ErrorMessages = UsersRepository.ErrorList;
                return View("Login");
            }
            else
            {
                return View("~/Views/SportEvent/SportEventList.cshtml");

            }
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (UsersRepository.AddUser(user))
            {
                ViewBag.SuccessMessages = "Konto zostało utworzone pomyślnie.";
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessages = UsersRepository.ErrorList;
            return View("Register");
        }
        public ActionResult PasswordReminder()
        {
            return View("PasswordReminder");
        }
    }
}