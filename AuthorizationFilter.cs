using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MyFirstBlog.Models
{
    //实现了授权过滤的特性类
    public class AuthorizationFilter:AuthorizeAttribute
    {
        //此方法逻辑处理，在此处为判断用户是否登录
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["valid"] != null)
                return true;
         
                else

                return false;
          
        }

        //此方法为没有取得授权所执行的操作
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //ContentResult contentResult = new ContentResult();
            //contentResult.Content = "meiyoudenglu";
            //filterContext.Result = contentResult;

            try
            {
                filterContext.HttpContext.Response.Redirect("/Login/Login");
               
            }
            catch (Exception)
            {

                return;
            }
         
        }
    }
}