using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
namespace QuanLyXe.Controllers
{
    public class sendnoteController : Controller
    {
        // GET: sendnote
        QLBanXeEntities3 db = new QLBanXeEntities3();
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult Sendnote()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Sendnote(Note n,FormCollection f)
        {
            //if(ModelState.IsValid)
            //{ 
            //    db.Notes.Add(n);
            //    db.SaveChanges();
            //}
            //return View();
            var hoten = f["txtten"].ToString();
            var email = f["txtemail"].ToString();
            var dienthoai = f["txtsdt"].ToString();
            var note = f["txtnote"].ToString();
            n.TenKH = hoten;
            n.Email = email;
            n.DienThoat = dienthoai;
            n.Comment = note;
            db.Notes.Add(n);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyLinhKien");
        }
    }
}