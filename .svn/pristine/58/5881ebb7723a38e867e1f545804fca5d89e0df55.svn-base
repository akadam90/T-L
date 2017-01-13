using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TestAndLearn.Web
{
    /// <summary>
    /// Use this attribute to enforce password changes for users.
    /// Place this attribute on any action controller to enforce password change
    /// when the logged in user meets the change requirements.
    /// </summary>
    public class EnforcePasswordChangePolicy : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MembershipUser user = Membership.GetUser(filterContext.HttpContext.User.Identity.Name);
            if (user.Comment == "changepassword" || user.LastPasswordChangedDate == user.CreationDate && Roles.IsUserInRole("Client"))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "ChangePassword" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}