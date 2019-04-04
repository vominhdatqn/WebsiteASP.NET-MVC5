using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using QuanLyXe.Areas.Admin.Models.NghiepVuModel;


namespace QuanLyXe.Areas.Admin.Controllers
{
    [Models.AuthorizeNghiepVu]
    public class NghiepVusController : Controller
    {
        private QLBanXeEntities3 db = new QLBanXeEntities3();

        public ActionResult UpdateNghiepVu()
        {
            ReflectionController rc = new ReflectionController();
            List<Type> listControllerType = rc.GetControllers("QuanLyXe.Areas.Admin.Controllers");
            List<string> listControllerOld = db.NghiepVus.Select(c => c.MaNghiepVu).ToList();
            List<string> listPermistionOld = db.BlogPermissions.Select(p => p.PermisstionName).ToList();
            foreach (var c in listControllerType)
            {
                if (!listControllerOld.Contains(c.Name))
                {
                   NghiepVu  nv = new NghiepVu() { MaNghiepVu = c.Name, TenNghiepVu = "Chưa có mô tả" };
                    db.NghiepVus.Add(nv);
                }
                List<string> listPermisstion = rc.GetActions(c);
                foreach (var p in listPermisstion)
                {
                    if (!listPermistionOld.Contains(c.Name +"-" + p))
                    {
                        BlogPermission permission = new BlogPermission() { PermisstionName= c.Name + "-" + p,Description="Chưa có mô tả",MaNghiepVu=c.Name};
                        db.BlogPermissions.Add(permission);
                    }
                }
            }
            db.SaveChanges();
            TempData["err"] = "<div class='alert alert-info' role='alert'><span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'></span><span class='sr-only'></span>Cập Nhật</div>";
            return RedirectToAction("Index");

        }

       
        // GET: Admin/NghiepVus
        public ActionResult Index()
        {
            return View(db.NghiepVus.ToList());
        }

        // GET: Admin/NghiepVus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NghiepVu nghiepVu = db.NghiepVus.Find(id);
            if (nghiepVu == null)
            {
                return HttpNotFound();
            }
            return View(nghiepVu);
        }

        // GET: Admin/NghiepVus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NghiepVus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNghiepVu,TenNghiepVu")] NghiepVu nghiepVu)
        {
            if (ModelState.IsValid)
            {
                db.NghiepVus.Add(nghiepVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nghiepVu);
        }

        // GET: Admin/NghiepVus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NghiepVu nghiepVu = db.NghiepVus.Find(id);
            if (nghiepVu == null)
            {
                return HttpNotFound();
            }
            return View(nghiepVu);
        }

        // POST: Admin/NghiepVus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNghiepVu,TenNghiepVu")] NghiepVu nghiepVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nghiepVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nghiepVu);
        }

        // GET: Admin/NghiepVus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NghiepVu nghiepVu = db.NghiepVus.Find(id);
            if (nghiepVu == null)
            {
                return HttpNotFound();
            }
            return View(nghiepVu);
        }

        // POST: Admin/NghiepVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NghiepVu nghiepVu = db.NghiepVus.Find(id);
            db.NghiepVus.Remove(nghiepVu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
