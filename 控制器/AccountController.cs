using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyFirstBlog.Models;
using Webdiyer.WebControls.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace MyFirstBlog.Controllers
{
  
    //添加授权过滤器，此控制器中的所有方法需要登录
    [AuthorizationFilter]
    public class AccountController : Controller
    {


        readonly VblogContext vblogContext = new VblogContext();
        //AccountController中的图片，用户id，用户名都来自LoginController
        static readonly LoginController Login = new LoginController();

      
       //以数组的形式返回图片地址，用户id，用户名
       static readonly string[] recevie = Login.GetArgs();
       
        static readonly string Img = recevie[0];
        static readonly int NowId = Convert.ToInt32(recevie[1]);
        static readonly string UserName=recevie[2];
        
        //展示文章方法
        public async Task<ActionResult> Index(int? pageIndex)       {

            var article = await vblogContext.Articles.ToListAsync();
            
            PagedList<Article> articles = new PagedList<Article>(article, pageIndex ?? 1, 4);
            
            return View(articles);
        }
       
        //测试数据方法
        public void CrtateDateTest()
        {
            //List<Admin> admins = new List<Admin>() {
            //    new Admin(){ AdName="张鹏", AdPasw="123456"}
            //};
            //admins.ForEach(a => vblogContext.Admins.Add(a));
            //vblogContext.SaveChanges();
            //List<User> users = new List<User>() {
            //    new User(){ HeadImg=@"~\Content\Imgs\2.jpg", UserName="张鹏3",PassWord="123456"}
            //};
            List<Article> articles = new List<Article>()
            {
                new Article(){ AddDateTime=DateTime.Now, Content="我脱单了", Hit=999, Title="热烈庆祝我校张鹏同学取得优异成绩", UserId=1}
            };
            articles.ForEach(a => vblogContext.Articles.Add(a));
            vblogContext.SaveChanges();
            //articles.ForEach(a => vblogContext.Articles.Add(a));
            //vblogContext.SaveChanges();
        }




        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Account/Create
        public ActionResult Create(int UseId)
        {
            var dropList = vblogContext.Users.Where(s => s.Id == UseId);
            ViewBag.UserId = new SelectList(dropList, "Id", "UserName");
            return View();
        }



        // POST: Account/Create
        //增加文章方法
        [HttpPost]
        public ActionResult Create(Article article)
        {

            if (ModelState.IsValid)
            {
                //vblogContext.Entry(article).State = System.Data.Entity.EntityState.Added;
                vblogContext.Articles.Add(article);
                vblogContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);


        }




        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }




        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, System.Web.Mvc.FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }





        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }





        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, System.Web.Mvc.FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        //Search
        [AuthorizationFilter]
        [HttpPost]
        public JsonResult Search(string searchName,int? pageIndex)
        {
            if (searchName != null)
            {
                var searchQuery = vblogContext.Articles.Where(a => a.Content.Contains(searchName)).ToList();
                if (searchQuery.Count == 0)
                {
                    //return HttpNotFound();
                }
                PagedList<Article> searchArticle = new PagedList<Article>(searchQuery, pageIndex ?? 1, 10);
                if (this.HttpContext.Request.IsAjaxRequest())
                {
                    return Json(searchArticle);
                }
               // return View("Index",searchArticle);
            }
            // return HttpNotFound();
            return Json(null);
        }
         


       

        //Method GetPerson
        #region 根据用户呈现到相对页面
        public ActionResult GetPerson(int? Id)
        {
            if (Id == null)
            { return HttpNotFound(); }
            else
            {
                var queryAdmin = vblogContext.Admins.Find(Id);
                var queryUser = vblogContext.Users.Find(Id);
                var register = vblogContext.RegisterUsers.Find(Id);
                if (queryAdmin != null)
                {
                    return View("Admin", queryAdmin);
                }
                if (queryUser != null)
                {
                    return View("User", queryUser);
                }
                if (register != null)
                {
                    return View("User", register);
                }
                return HttpNotFound();

            }
        }
        #endregion


        //GET  Method
        public ActionResult UpdateImg()
        {
            return View();
        }

        //更新头像方法
        [HttpPost]
        public ActionResult UpdateImg(HttpPostedFileBase file, int id)
        {
            //获取上传的文件名
            var fileName = file.FileName;
            //文件名裁剪
            string[] s= fileName.Split('.');
            if(s[1]=="jpg")
            {
                //返回~\Content\Imgs路径字符串
                var filePath = Server.MapPath(@"~\Content\Imgs");
                //将~\Content\Imgs路径与上传的文件名拼接
                string LastUpdate = Path.Combine(filePath, fileName);
                //将完整文件路径存储
                file.SaveAs(LastUpdate);

                 

                //修改当前用户的头像
                var UserImg = vblogContext.Users.Where(a => a.Id == id).FirstOrDefault();
                //将上传的文件名与\Content\Imgs\路径进行拼接
                UserImg.HeadImg = @"\Content\Imgs\" + fileName;
                //保存更改后的头像地址
                vblogContext.SaveChanges();

                return RedirectToAction("Index", "Account");
            }
            ViewBag.Err = "格式错误，只能是jpg";
            return View("~/Views/Shared/NotValidError.cshtml");
           
        }

        //个人中心方法
        public ActionResult UserCenter(int id)
        {
            var userCenList = vblogContext.Users.Where(a => a.Id == id);
            ViewBag.Img = vblogContext.Users.Where(a => a.Id == id).Select(a => a.HeadImg).First();
            return View(userCenList);
        }


        //分部视图
        public PartialViewResult Part()
        {

            ViewBag.PartImg = Img;
            ViewBag.NowId = NowId;
            ViewBag.UserName = UserName;
            return PartialView();
        }

        //Get Now User Id
        public int GetUserId()
        {
            //var article = vblogContext.Articles.Include("User").FirstOrDefault();
            //返回当前用户id
            return NowId;
        }

        //用户退出
        public ActionResult LogOut()
        {

            Session["valid"] = null;
            return RedirectToAction("Login","Login");
        }
        public ActionResult Ajax()
        {
            return View();
        }
    }  
}
