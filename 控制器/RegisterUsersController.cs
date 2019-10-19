using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MyFirstBlog.Models;

namespace MyFirstBlog.Controllers
{
    public class RegisterUsersController : Controller
    {
        private VblogContext vblogContext = new VblogContext();
        #region 注册模块
        //Register
        public ActionResult Register()
        {
            return View();
        }
        //httpPost
        [HttpPost]
        public ActionResult Register(RegisterUser registerUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MD5 mD5 = MD5.Create())
                    {
                        byte[] RegisterUser = Encoding.UTF8.GetBytes(registerUser.PassWord);
                        byte[] vs = mD5.ComputeHash(RegisterUser);

                        byte[] ConfirmPw = Encoding.UTF8.GetBytes(registerUser.ConfirmPassWord);
                        byte[] vs1 = mD5.ComputeHash(ConfirmPw);

                        RegisterUser user = new RegisterUser(){ Id=registerUser.Id,UserName = registerUser.UserName, PassWord =Convert.ToBase64String(vs), ConfirmPassWord = Convert.ToBase64String(vs1) };
                        vblogContext.RegisterUsers.Add(user);
                        vblogContext.SaveChanges();

                    }

                  

                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View(registerUser);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMs = ex.Message;
                return View("~/Views/Account/Error.cshtml");

            }

        }
        #endregion


    }
}
