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
            return View();
        }


        public ActionResult Login(string email, string password)
        {
            bool emailAndPasswordAreEmpty = string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password);
            bool passwordIsCorrect = emailAndPasswordAreEmpty ? false : UsersRepository.ValidatePasswordIsCorrect(email, password);
            if (emailAndPasswordAreEmpty || !passwordIsCorrect)
            {
                ViewBag.ErrorMessages = UsersRepository.ErrorList;
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "SportEvent");

            }
        }

        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
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
        public ActionResult PasswordReminder()
        {
            return View("PasswordReminder");
        }
    }
}