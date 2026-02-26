using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTrainingProject.Filters
{
    public class AuthFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            // Session check
            if (httpContext.Session["User"] != null)
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirect to Login page
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}