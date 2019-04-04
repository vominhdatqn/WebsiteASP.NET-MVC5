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
    public class NhaCungCapController : Controller
    {
        // GET: NhaCungCap
        QLBanXeEntities3 db = new QLBanXeEntities3();
        public PartialViewResult NhaCungCapPartial()
        {   
            return PartialView(db.NhaCungCaps.Take(5).OrderBy(n => n.TenNCC).ToList());
        }

        public ViewResult SanPhamNCC(string MaNCC,int? page)
        {
            NhaCungCap NCC = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == MaNCC);

            if (NCC == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            int pageSize = 6;
            // tạo biến số trang
            int pageNumber = (page ?? 1);
            //truy xuat  danh sach cac quyen sach theo nha cc
            List<SanPham> lstSanPham = db.SanPhams.Where(n => n.MaNCC == MaNCC).OrderBy(n => n.GiaBan).ToList();

            if (lstSanPham.Count == 0)
            {
                ViewBag.SanPham = "Không có sản phảm nào thuộc nhà cung cấp này!!!";
                return View(db.SanPhams.OrderBy(n => n.MaNCC).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.NhaCC = MaNCC;
    
            return View(lstSanPham.ToPagedList(pageNumber, pageSize));

        }
        public PartialViewResult DanhNhaCC()
        {
            return PartialView(db.NhaCungCaps.ToList());

        }
    }
}