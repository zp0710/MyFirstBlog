using System.Linq;
using System.Web.Mvc;
using MyFirstBlog.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System;

namespace MyFirstBlog.Controllers
{
    public class LoginController : Controller
    {
      private readonly  VblogContext vblogContext = new VblogContext();

        static string Img;
        static int NowId;
        static string UserName;

        // GET: Login
        #region 登录模块
        public ActionResult Login()
        {
            //CrtateDateTest();

            return View();
        }
        //Post:Login
        [HttpPost, ActionName("Login")]
        public async Task<ActionResult> LoginTo(string userName, string userPassword)
        {
           
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPassword))
            {
                //对输入的密码进行加密后与数据库进行比对
                # region
                MD5 mD5 = MD5.Create();
                byte[] UserMd5 = Encoding.UTF8.GetBytes(userPassword);
                byte[] deEncryPw = mD5.ComputeHash(UserMd5);
                string DeEncryUserPw = Convert.ToBase64String(deEncryPw);
                #endregion
                //在管理员表中查找是否有对应的管理员
                var query =await vblogContext.Admins.FirstOrDefaultAsync(a => a.AdName == userName && a.AdPasw == userPassword);



                //在用户表中查找是否有对应的用户
                var queryUser =await vblogContext.Users.FirstOrDefaultAsync(a => a.UserName == userName && a.PassWord == userPassword);



                var registerUser = vblogContext.RegisterUsers.Where(a => a.UserName == userName && a.PassWord == DeEncryUserPw);
                //回收MD5
                mD5.Dispose();
                if(query!=null)
                {
                    Img = queryUser.HeadImg;
                    NowId = queryUser.Id;
                    UserName = queryUser.UserName;
                }
               

                if (query!=null) //是否为管理员
                {
                    Session["Adminvalid"] = "true";
                    return RedirectToAction("Index", "Admin");
                }
                else
                if (queryUser!=null)//是否为用户
                {
                    Session["valid"] = "true";
                    Response.Clear();
                    return RedirectToAction("Index", "Account");
                }
                else if (registerUser.Count() > 0)
                {
                    Session["valid"] = "true";
                    return RedirectToAction("Index", "Account");
                }
                return HttpNotFound();
            }
            return HttpNotFound();
        }
        #endregion

        public string[] GetArgs()
        {
            string[] args ={Img,NowId.ToString(),UserName };
            return args;
        }
    }
}