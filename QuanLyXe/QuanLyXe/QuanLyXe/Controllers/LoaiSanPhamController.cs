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
    public class LoaiSanPhamController : Controller
    {
        // GET: LoaiSanPham
        QLBanXeEntities3 db = new QLBanXeEntities3();
        public PartialViewResult LoaiSanPhamPartial()
        {
            //return PartialView(db.LoaiSanPhams.Take(3).OrderBy(n => n.TenLoai).ToList());
            return PartialView(db.LoaiSanPhams.ToList());
        }
        public ActionResult SanPhamTheoLoai(string MaLoai, int? page)
        {
            //kiem tra thu loai sp co toan tai k?
            LoaiSanPham lsp = db.LoaiSanPhams.SingleOrDefault(n => n.MaLoai == MaLoai);

            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            int pageSize = 6;
            // tạo biến số trang
            int pageNumber = (page ?? 1);
            //truy xuat  danh sach cac quyen sach theo loai sp
            List<SanPham> lsSanPham = db.SanPhams.Where(n => n.MaLoai == MaLoai).OrderByDescending(n => n.GiaBan).ToList();
            {
                if (lsSanPham.Count == 0)
                {
                    ViewBag.SanPham = "Không có sản phảm nào thuộc loại này!!!";
                    //ViewBag.SanPham = "Không có sản phảm nào thuộc loại này!!!";

                    return View(db.SanPhams.OrderBy(n => n.MaLoai).ToPagedList(pageNumber, pageSize));
                }
                //ViewBag.SanPham = "Đã Tìm Thấy " + lsSanPham.Count + "Kết Quả!";
                //return View(lsSanPham.OrderBy(n => n.MaLoai).ToPagedList(pageNumber, pageSize));
                //return View(lsSanPham);

            }
            ViewBag.SanPham = MaLoai;
            return View(lsSanPham.ToPagedList(pageNumber, pageSize));

        }

        //public ViewResult DanhMucLoai()
        //{
        //    return View(db.LoaiSanPhams.ToList());

        //}
    }
}