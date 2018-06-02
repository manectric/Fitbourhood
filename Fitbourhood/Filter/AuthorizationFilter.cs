using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fitbourhood.Controllers;
using Fitbourhood.Helpers;
using Fitbourhood.Models;

namespace Fitbourhood.Filter
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserContextModel user = UserContextHelper.GetUserContextModel();
            if (user == null)
            {
                var controller = new HomeController();
                filterContext.Result = controller.RedirectToAction("Index", "Home");
            }
        }

    }
}