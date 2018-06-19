using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitbourhood.Filter;
using Fitbourhood.Helpers;
using Fitbourhood.Models;
using Fitbourhood.Repositories;

namespace Fitbourhood.Controllers
{
    [AuthorizationFilter]
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        [HttpGet]
        public ActionResult Index()
        {
            UserProfileModel model = new UserProfileModel();
            model = UsersRepository.GetUserProfileModel(UserContextHelper.GetUserContextModel().ID);
            return View("UserProfile", model);
        }

        public ActionResult Achievements()
        {
            var achievementList = UsersRepository.GetUserAchievements(UserContextHelper.GetUserContextModel().ID);
            return View("Achievements", achievementList);
        }

        public JsonResult GetNotifications()
        {
            var notifications = UsersRepository.GetNotificationsForUser(UserContextHelper.GetUserContextModel().ID);
            return Json(notifications);
        }

        public JsonResult RespondToNotification(int userId, int sportEventId, string description)
        {
            bool result = false;

            result = UsersRepository.RespondToNotification(userId, sportEventId, description);

            return Json(result);
        }

        [HttpPost]
        public ActionResult ChangePassword(UserProfileModel model)
        {
            if (UsersRepository.ValidatePasswordIsCorrect(UserContextHelper.GetUserContextModel().Email,
                    model.OldPassword) != null)
            {
                if (UsersRepository.ChangePassword(model.NewPassword, UserContextHelper.GetUserContextModel().ID))
                {
                    ModelState.Clear();
                    ViewBag.SuccessMessages = "Zmiana hasła zakończona pomyślnie";
                }
            }

            model = UsersRepository.GetUserProfileModel(UserContextHelper.GetUserContextModel().ID);
            ViewBag.ErrorMessages = UsersRepository.ErrorList;
            return View("UserProfile", model);
        }

    }
}