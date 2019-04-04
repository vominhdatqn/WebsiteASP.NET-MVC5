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
    public class QuanLyLinhKienController : Controller
    {
        // GET: QuanLyLinhKien
        QLBanXeEntities3 db = new QLBanXeEntities3();
        public ActionResult Index(int? page)
        {
            // tạo biến số sản phẩm trên trang
            int pageSize = 6;
            // tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.SanPhams.Where(n => n.SanPhamMoi == 1).OrderBy(n => n.GiaBan).ToPagedList(pageNumber, pageSize));
        }

    }
}