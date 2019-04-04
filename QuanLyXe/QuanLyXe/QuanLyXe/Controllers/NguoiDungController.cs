using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Security;

namespace QuanLyXe.Controllers
{
    public class NguoiDungController : Controller
    {
        QLBanXeEntities3 db = new QLBanXeEntities3();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh,FormCollection f)
        {
            var hoten = f["txtTen"].ToString();
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", f["NgaySinh"]);
            var dienthoai = f["txtPhone"].ToString();
            var diachi = f["txtDiaChi"].ToString();
            var gioitinh = f["txtGioiTinh"].ToString();
            var email = f["txtEmail"].ToString();
            var taikhoan = f["txtTK"].ToString();
            var matkhau = f["txtMK"].ToString();
            var admin = int.Parse("0".ToString());
            var chophep = int.Parse("0".ToString());
            //var admin = int.Parse(f["txtadmin"].ToString());
            //var chophep = int.Parse(f["txtchophep"].ToString());
            kh.TenKH = hoten;
            kh.NgaySinh = DateTime.Parse(ngaysinh);
            kh.DienThoai = dienthoai;
            kh.DiaChi = diachi;
            kh.GioiTinh = gioitinh;
            kh.Email = email;
            kh.TaiKhoan = taikhoan;
            kh.MatKhau = matkhau;
            kh.isAdmin = admin;
            kh.Allowed = chophep;
            db.KhachHangs.Add(kh);
            //Luu vaoo csdl
            db.SaveChanges();
            return RedirectToAction("DangNhap","NguoiDung");

        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //var response = Request["g-recaptcha-response"];
            //string secretkey = "6LcFTVQUAAAAACWEzR-PLUvO0IOPCsmXeVxe61lR";
            //var client = new WebClient();
            //var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretkey, response));
            //var obj = JObject.Parse(result);
            //var status = (bool)obj.SelectToken("success");
            //ViewBag.Message = status ? "google captcha true" : "google captcha faile";
            string iTaiKhoan = f["txtTaikhoan"].ToString();
            string iMatKhau = f.Get("txtMatkhau");
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == iTaiKhoan && n.MatKhau == iMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "chúc mừng bạn đăng nhập thành công !";

                Session["MaKH"] = kh;
                Session["TaiKhoan"] = kh.TaiKhoan;
                Session["TenKH"] = kh.TenKH;
                ViewBag.ThongTin = Session["TaiKhoan"].ToString();
                return RedirectToAction("Index", "QuanLyLinhKien");
            }
            ViewBag.ThongBao = "tên tài khoản hoặc mật khẩu không đúng!";
            return View();
            //public ActionResult DangNhap(FormCollection f)
            //{
            //    var response = Request["g-recaptcha-response"];
            //    string secretkey = "your secret key here";
            //    var client = new WebClient();
            //    var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretkey, response));
            //    var obj = JObject.Parse(result);
            //    var status = (bool)obj.SelectToken("success");
            //    ViewBag.Message = status ? "google captcha true" : "google captcha faile";
            //    string iTaiKhoan = f["txtTaikhoan"].ToString();
            //    string iMatKhau = f.Get("txtMatkhau");
            //    var user = db.KhachHangs.SingleOrDefault(x => x.TaiKhoan == iTaiKhoan && x.MatKhau == iMatKhau && x.Allowed == 1);
            //    KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == iTaiKhoan && n.MatKhau == iMatKhau);
            //    if (user!=null)
            //    {
            //        Session["TaiKhoan"] = user.TaiKhoan;
            //        Session["MatKhau"] = user.MatKhau;
            //        Session["TenKH"] = user.TenKH;
            //        Session["MaKH"] = user.MaKH;
            //        return RedirectToAction("Admin/Home/Index");
            //    }
            //   else if (kh != null)
            //    {
            //        ViewBag.ThongBao = "chúc mừng bạn đăng nhập thành công !";

            //        Session["MaKH"] = kh;
            //        Session["TaiKhoan"] = kh.TaiKhoan;
            //        ViewBag.ThongTin = Session["TaiKhoan"].ToString();
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    ViewBag.ThongBao = "tên tài khoản hoặc mật khẩu không đúng!";
            //    return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DangNhap(KhachHang objUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (QLBanXeEntities db = new QLBanXeEntities())
        //        {
        //            var obj = db.KhachHangs.Where(a => a.TaiKhoan.Equals(objUser.TaiKhoan) && a.MatKhau.Equals(objUser.MatKhau)).FirstOrDefault();
        //            if (obj != null)
        //            {
        //                Session["TaiKhoan"] = obj.TaiKhoan.ToString();
        //                return RedirectToAction("UserDashBoard");
        //            }
        //        }
        //    }
        //    return View(objUser);
        //}

        public ActionResult HienThi()
        {
            KhachHang kh = (KhachHang)Session["MaKH"];
            if (kh != null)
            {
                var khachhang = db.KhachHangs.First(n => n.MaKH == kh.MaKH);
                return View(khachhang);
            }
            else
            return RedirectToAction("Index", "QuanLyLinhKien");
        }
        [HttpGet]
        public ActionResult SuaThongTin(int id)
        {

            var khachhang = db.KhachHangs.First(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost]
        public ActionResult SuaThongTin(int id,FormCollection f)
        {
           
                var khachhang = db.KhachHangs.First(n => n.MaKH == id);
                khachhang.TenKH = f.Get("txtten").ToString();
                khachhang.NgaySinh = DateTime.Parse(f.Get("ngaysinh"));
                khachhang.DienThoai = f.Get("txtphone").ToString();
                khachhang.DiaChi = f.Get("txtdiachi").ToString();
                db.SaveChanges();


            return RedirectToAction("HienThi");
        }
        public ActionResult DangXuat()
        {
            Session.Remove("MaKH");
            return RedirectToAction("Index", "QuanLyLinhKien");
        }
        //public ActionResult Sendnote()
        //{
        //    return View();
        //}
        //public ActionResult Sendnote(Note n, FormCollection f)
        //{
        //    var hoten = f["txtten"].ToString();
        //    var email = f["txtemail"].ToString();
        //    var dienthoai = f["txtsdt"].ToString();
        //    var note = f["txtnote"].ToString();
        //    n.TenKH = hoten;
        //    n.Email = email;
        //    n.DienThoat = dienthoai;
        //    n.Comment = note;
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "QuanLyLinhKien");
        //}
    }
}