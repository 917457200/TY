using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Core
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class LoginNeedsFilter : ActionFilterAttribute
    {
        public bool IsCheck { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            if (IsCheck)
            {
                var cookies = HttpContext.Current.Request.Cookies["Dfbg_OAUser"]; //创建Cookie并命名
                if (cookies == null)
                {
                    filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "home", action = "login" }));
                }
               
            }
            base.OnActionExecuting(filterContext);
        }

    }
}