using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFirstBlog.Models;
using MyFirstBlog.Controllers;

namespace MyFirstBlog.Controllers
{
    [AuthorizationFilter]  //使用了授权过滤特性，该方法必须先登录才能访问
    public class GuestController : Controller
    {
        VblogContext Vblog = new VblogContext();
        static int UserId;
        // GET: Guest
      
        public ActionResult Index()
        {
           
            AccountController accountController = new AccountController();

            UserId = accountController.GetUserId();

            var NowUser = Vblog.Users.Where(a => a.Id == UserId);

            ViewBag.UserId = new SelectList(NowUser, "Id", "UserName");

            return View(Vblog.Guests.ToList());

            //return View("~/Views/Shared/NotValidError.cshtml");
        }

        [HttpPost,ActionName("Index")]
        public  ActionResult IndexTo(Guest guest)
        {
            Vblog.Guests.Add(guest);
            Vblog.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Guest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Guest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Guest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Guest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Guest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Guest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
