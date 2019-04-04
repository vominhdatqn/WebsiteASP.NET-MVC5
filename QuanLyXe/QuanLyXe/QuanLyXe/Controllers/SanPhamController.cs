using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
namespace QuanLyXe.Controllers
{
    public class SanPhamController : Controller
    {
        QLBanXeEntities3 db = new QLBanXeEntities3();
        // GET: SanPham
        public PartialViewResult SanPhamMoiPartial()
        {
            var lstSanPhamMoi = db.SanPhams.Take(3).ToList();
            return PartialView(lstSanPhamMoi);
        }
        public ViewResult XemChiTietSanPham(int MaSP)
        {
            SanPham SanPham = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (SanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // LoaiSanPham lsp = db.LoaiSanPhams.Single(n => n.MaLoai == SanPham.MaLoai);
            //ViewBag.TenLoai = lsp.TenLoai;
            ViewBag.TenLoai = db.LoaiSanPhams.Single(n => n.MaLoai == SanPham.MaLoai).TenLoai;
            ViewBag.NhaCC = db.NhaCungCaps.Single(n => n.MaNCC == SanPham.MaNCC).TenNCC;
            return View(SanPham);

        }
    }
}