using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using QuanLyXe.Areas.Admin.Models;

namespace QuanLyXe.Areas.Admin.Controllers
{
    [AuthorizeNghiepVu]
    public class BlogPermissionsController : Controller
    {
        
        private QLBanXeEntities3 db = new QLBanXeEntities3();

        // GET: Admin/BlogPermissions
        public ActionResult Index(string id)
        {

            var blogPermissions = db.BlogPermissions.Where(x => x.MaNghiepVu == id);
            return View(blogPermissions.ToList());
        }

        // GET: Admin/BlogPermissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPermission blogPermission = db.BlogPermissions.Find(id);
            if (blogPermission == null)
            {
                return HttpNotFound();
            }
            return View(blogPermission);
        }

        // GET: Admin/BlogPermissions/Create
        public ActionResult Create()
        {
            ViewBag.MaNghiepVu = new SelectList(db.NghiepVus, "MaNghiepVu", "TenNghiepVu");
            return View();
        }

        // POST: Admin/BlogPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PermissionId,PermisstionName,Description,MaNghiepVu")] BlogPermission blogPermission)
        {
            if (ModelState.IsValid)
            {
                db.BlogPermissions.Add(blogPermission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNghiepVu = new SelectList(db.NghiepVus, "MaNghiepVu", "TenNghiepVu", blogPermission.MaNghiepVu);
            return View(blogPermission);
        }

        // GET: Admin/BlogPermissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPermission blogPermission = db.BlogPermissions.Find(id);
            if (blogPermission == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNghiepVu = new SelectList(db.NghiepVus, "MaNghiepVu", "TenNghiepVu", blogPermission.MaNghiepVu);
            return View(blogPermission);
        }

        // POST: Admin/BlogPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PermissionId,PermisstionName,Description,MaNghiepVu")] BlogPermission blogPermission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPermission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id=blogPermission.MaNghiepVu});
            }
            ViewBag.MaNghiepVu = new SelectList(db.NghiepVus, "MaNghiepVu", "TenNghiepVu", blogPermission.MaNghiepVu);
            return View(blogPermission);
        }

        // GET: Admin/BlogPermissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPermission blogPermission = db.BlogPermissions.Find(id);
            if (blogPermission == null)
            {
                return HttpNotFound();
            }
            return View(blogPermission);
        }

        // POST: Admin/BlogPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPermission blogPermission = db.BlogPermissions.Find(id);
            db.BlogPermissions.Remove(blogPermission);
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
