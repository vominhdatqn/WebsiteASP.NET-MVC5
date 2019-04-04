using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using PagedList;
using PagedList.Mvc;
namespace QuanLyXe.Controllers
{
    public class HomeController : Controller
    {
       QLBanXeEntities5 db = new QLBanXeEntities5();
        public ActionResult Index(int? page)
        {
            // tạo biến số sản phẩm trên trang
            int pageSize = 6;
            // tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.SanPhams.Where(n => n.SanPhamMoi == 1).OrderBy(n => n.GiaBan).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}