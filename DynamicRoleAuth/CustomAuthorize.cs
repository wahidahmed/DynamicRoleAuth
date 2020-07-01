using DynamicRoleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DynamicRoleAuth
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {
        DefaultConnection dbs = new DefaultConnection();
        //Custom named parameters for annotation
        public string Source { get; set; }//Controller Name
        public string Function { get; set; }//Action Name


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Is user logged in?
            if (httpContext.User.Identity.IsAuthenticated)
            {

                if ((!string.IsNullOrEmpty(Source)) && (!string.IsNullOrEmpty(Function)))
                {
                    //There are many ways to store and validate RoleRights 
                    //1.You can store in Database and validate from Database.
                    //2.You can store in user claim at the time of login and validate from UserClaims.
                    //3.You can store in session validate from session

                    //Below I am using database approach.
                    var loggedInUserRoles = ((ClaimsIdentity)httpContext.User.Identity).Claims
                                            .Where(c => c.Type == ClaimTypes.Role)
                                            .Select(c => c.Value);

                    //logic to check loggedInUserRoles has rights or not from RoleRights table

                    //return db.RoleRights.Any(x => x.AppContent.Source == Source && x.AppContent.Function == Function && loggedInUserRoles.Contains(x.AppContent.RoleName));

                    return dbs.RoleRights.Any(x => x.AppContent.Controller == Source && x.AppContent.Action == Function && loggedInUserRoles.Contains(x.RoleName));

                }

            }
            //Returns true or false, meaning allow or deny. False will call HandleUnauthorizedRequest above

            return base.AuthorizeCore(httpContext);
        }

        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
                return;

            }
            //User is logged in but has no access
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "NotAuthorized" })
                );
            }

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Check for authorization

            if (string.IsNullOrEmpty(this.Source) && string.IsNullOrEmpty(this.Function))
            {
                this.Source = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                this.Function = filterContext.ActionDescriptor.ActionName;
            }

            base.OnAuthorization(filterContext);
        }
    }
}