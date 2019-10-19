using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyFirstBlog.Models;
using System.Data.Entity;

namespace MyFirstBlog.Controllers
{
    [AdminAuthor]
    public class AdminController : Controller
    {
        readonly VblogContext vblogContext = new VblogContext();
        // GET: Admin
        public ActionResult Index()
        {
            
            return View(vblogContext.Articles.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
          Article article=  vblogContext.Articles.Find(id);

            return View(article);
        }

        // POST: Admin/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Article articles = vblogContext.Articles.Find(id);
            vblogContext.Articles.Remove(articles);
            vblogContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult about()
        {
            return View();
        }
        public async Task<ActionResult> ShowUser()
        {
            return View(await vblogContext.Users.ToListAsync());
        }
    }
}
