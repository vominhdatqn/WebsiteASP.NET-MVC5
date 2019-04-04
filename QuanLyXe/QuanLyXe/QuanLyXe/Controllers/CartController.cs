using QuanLyXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyXe.Controllers
{
    public class CartController : Controller
    {
        QLBanXeEntities3 db = new QLBanXeEntities3();
        public ActionResult Index()
        {
            var cart = ShoppingCart.Cart;
            return View(cart.Items);
        }
        public ActionResult Add(int id)
        {
            var cart = ShoppingCart.Cart;
            cart.Add(id);

            var info = new {
                cart.Count
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult Remove(int id)
        {
            var cart = ShoppingCart.Cart;
            cart.Remove(id);

            var info = new {
                cart.Count,
                tongsl = cart.Items.Sum(n => n.SoLuongTon),
                tongtien = cart.Items.Sum(m => m.GiaBan * m.SoLuongTon).ToString("#,##0").Replace(',', '.')
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(int id, int quantity)
        {
            var cart = ShoppingCart.Cart;
            cart.Update(id, quantity);

            var p = cart.Items.Single(i => i.MaSP == id);
            var info = new
            {
                tongsl= cart.Items.Sum(n=>n.SoLuongTon),
                tongtien=cart.Items.Sum(m=>m.GiaBan*m.SoLuongTon).ToString("#,##0").Replace(',', '.'),
                Amount = (p.GiaBan*p.SoLuongTon).ToString("#,##0").Replace(',', '.')
            };
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public ActionResult XoaGioHang(int iMaSP)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cart = ShoppingCart.Cart;
            var sanpham = cart.Items.SingleOrDefault(n => n.MaSP == iMaSP);
            if (sanpham != null)
            {
                cart.Items.RemoveAll(n => n.MaSP == iMaSP);

            }

            if (cart.Items.Count == 0)
            {
                return RedirectToAction("Index", "QuanLyLinhKien");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Clear()
        {
            var cart = ShoppingCart.Cart;
            cart.Items.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult XoaTatCaGioHang()
        {
            var cart = ShoppingCart.Cart;
            cart.Items.Clear();
            return RedirectToAction("Index", "QuanLyLinhKien");
        }
        #region Dat Hang
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "QuanLyLinhKien");
            }

            //Lay gio hang tu Session
            var cart = ShoppingCart.Cart;
            return View(cart.Items);
            //List<GioHang> lstGiohang = LayGioHang();
        }
        // xay dung chac nang dat hang
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang)Session["MaKH"];
            //List<GioHang> gh = LayGioHang();
            ShoppingCart gh = (ShoppingCart)Session["GioHang"];
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.TinhTrangGH = false;
            ddh.DaThanhToan = false;
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            //Them chi tiet don hang            
            foreach (var item in gh.Items)
            {
                ChiTietSanPham ctdh = new ChiTietSanPham();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaSP = item.MaSP;
                ctdh.SoLuong = item.SoLuongTon;
                ctdh.DonGia = (decimal)item.GiaBan;

                db.ChiTietSanPhams.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Xacnhandonhang", "Cart");
        }

        #endregion
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}