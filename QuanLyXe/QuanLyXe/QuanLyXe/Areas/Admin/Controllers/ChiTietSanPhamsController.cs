using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;


namespace QuanLyXe.Areas.Admin.Controllers
{
    [Models.AuthorizeNghiepVu]
    public class ChiTietSanPhamsController : Controller
    {
        private QLBanXeEntities3 db = new QLBanXeEntities3();

        // GET: Admin/ChiTietSanPhams
        public ActionResult Index()
        {
            var chiTietSanPhams = db.ChiTietSanPhams.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(chiTietSanPhams.ToList());
        }

        // GET: Admin/ChiTietSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSanPham chiTietSanPham = db.ChiTietSanPhams.Find(id);
            if (chiTietSanPham == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSanPham);
        }

        // GET: Admin/ChiTietSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "MaDonHang");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/ChiTietSanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaDonHang,SoLuong,DonGia")] ChiTietSanPham chiTietSanPham)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietSanPhams.Add(chiTietSanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "MaDonHang", chiTietSanPham.MaDonHang);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietSanPham.MaSP);
            return View(chiTietSanPham);
        }

        // GET: Admin/ChiTietSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSanPham chiTietSanPham = db.ChiTietSanPhams.Find(id);
            if (chiTietSanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "MaDonHang", chiTietSanPham.MaDonHang);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietSanPham.MaSP);
            return View(chiTietSanPham);
        }

        // POST: Admin/ChiTietSanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaDonHang,SoLuong,DonGia")] ChiTietSanPham chiTietSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "MaDonHang", chiTietSanPham.MaDonHang);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietSanPham.MaSP);
            return View(chiTietSanPham);
        }

        // GET: Admin/ChiTietSanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietSanPham chiTietSanPham = db.ChiTietSanPhams.Find(id);
            if (chiTietSanPham == null)
            {
                return HttpNotFound();
            }
            return View(chiTietSanPham);
        }

        // POST: Admin/ChiTietSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietSanPham chiTietSanPham = db.ChiTietSanPhams.Find(id);
            db.ChiTietSanPhams.Remove(chiTietSanPham);
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
