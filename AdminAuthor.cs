using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstBlog.Models
{
    public class AdminAuthor: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (httpContext.Session["Adminvalid"]!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            try
            {
                filterContext.HttpContext.Response.Redirect("/Login/Login");
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}