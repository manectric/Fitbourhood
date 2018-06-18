using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitbourhood.Filter;
using Fitbourhood.Helpers;
using Fitbourhood.Repositories;

namespace Fitbourhood.Controllers
{
    [AuthorizationFilter]
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            return View("UserProfile");
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

    }
}