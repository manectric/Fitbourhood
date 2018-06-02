using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fitbourhood.Models;
using System.Web.Mvc;

namespace Fitbourhood.Helpers
{
    public static class UserContextHelper
    {
        public static void SetUserSession(UserContextModel user)
        {
            System.Web.HttpContext.Current.Session["CurrentUser"] = user;
        }

        public static UserContextModel GetUserContextModel()
        {
            return (UserContextModel)System.Web.HttpContext.Current.Session["CurrentUser"];
        }
    }
}