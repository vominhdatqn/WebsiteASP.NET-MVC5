using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
//using System.Web.Script.Services;
//using System.Web.Services;

namespace QuanLyXe.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QLBanXeEntities3 db = new QLBanXeEntities3();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var madonhang = (from p in db.DonHangs
                             select p.MaDonHang).Count();
            ViewBag.NewOder = madonhang;
            var tongdoanhso = (from n in db.ChiTietSanPhams
                               select n.SoLuong).Sum();
            ViewBag.TongSL = tongdoanhso;
            var tongkhach = (from o in db.KhachHangs
                             where o.isAdmin == 0
                             select o.MaKH).Count();
            ViewBag.User = tongkhach;
            var ThanhTien = (from a in db.ChiTietSanPhams
                             join g in db.SanPhams on a.MaSP equals g.MaSP
                             where a.MaSP == g.MaSP
                             select a.SoLuong * a.DonGia).Sum();
                             
            ViewBag.ThanhTien = ThanhTien;
            DateTime tn = DateTime.Now;
            ViewBag.Data = tn.ToString("dd/MM/yyyy");
          
                                     
            return View();

        }


        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public static List<object> GetChartData()
        //{
        //    string query = "SELECT MaSP, COUNT(SoLuong) SoLuong";
        //    query += " FROM ChiTietSanPhams WHERE MaSP = '1' GROUP BY MaSP";
        //    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    List<object> chartData = new List<object>();
        //    chartData.Add(new object[]
        //    {
        //"Mã Sản Phẩm", "SoLuong"
        //    });
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Connection = con;
        //            con.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    chartData.Add(new object[]
        //                    {
        //                sdr["ShipCity"], sdr["TotalOrders"]
        //                    });
        //                }
        //            }
        //            con.Close();
        //            return chartData;
        //        }
        //    }
        //}
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(string taikhoan,string matkhau)
        {
            //var response = Request["g-recaptcha-response"];
            //string secretkey = "6LcFTVQUAAAAACWEzR-PLUvO0IOPCsmXeVxe61lR";
            //var client = new WebClient();
            //var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretkey, response));
            //var obj = JObject.Parse(result);
            //var status = (bool)obj.SelectToken("success");
            //ViewBag.Message = status ? "google captcha true" : "google captcha faile";
            var user = db.KhachHangs.SingleOrDefault(x=>x.TaiKhoan==taikhoan&&  x.MatKhau==matkhau&&x.Allowed==1);
            if (user!=null)
            {
                Session["TaiKhoan"] = user.TaiKhoan;
                Session["MatKhau"] = user.MatKhau;
                Session["TenKH"] = user.TenKH;
                Session["MaKH"] = user.MaKH; 
    
                return RedirectToAction("Index");
            }
            ViewBag.error = "Đăng Nhập Sai Hoặc Bạn Không Có Quyền Hạn";
            return View();

        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            Session["MatKhau"] = null;
            Session["TenKH"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult DoiMatKhau()
        {
            //lay doi tuong
            KhachHang ad = (KhachHang)Session["TaiKhoan"];
            return View(ad);
        }
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection collection)
        {
            KhachHang ad = (KhachHang)Session["TaiKhoan"];
            ad.MatKhau = collection["MatKhau"];
            var admin = db.KhachHangs.First(k => k.MaKH == ad.MaKH);
            admin.MatKhau = ad.MatKhau;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult NotificationAuthorize()
        {
            return View();
        }
        public EmptyResult Alive()
        {
            return new EmptyResult();
        }
        public ActionResult PieChart()
        {
            return View();
        }

        public ActionResult GetData()
        {
            int sp1 = db.ChiTietSanPhams.Where(x => x.MaSP == 1|| x.MaSP==3 || x.MaSP == 4 || x.MaSP == 5 || x.MaSP == 6 || x.MaSP == 7 || x.MaSP == 8 || x.MaSP == 9  || x.MaSP == 10 || x.MaSP == 11 || x.MaSP == 14).Count();
          
          
            int sp3 = db.ChiTietSanPhams.Where(x => x.MaSP == 16 || x.MaSP == 18 || x.MaSP == 21 || x.MaSP == 22 || x.MaSP == 36 || x.MaSP == 37 || x.MaSP == 38 || x.MaSP == 39 || x.MaSP == 40 || x.MaSP == 41).Count();
            int sp4 = db.ChiTietSanPhams.Where(x => x.MaSP == 61 || x.MaSP == 62 || x.MaSP == 63 || x.MaSP == 64 || x.MaSP == 65 || x.MaSP == 66 || x.MaSP == 67 || x.MaSP == 68 || x.MaSP == 69 || x.MaSP == 71).Count();
            int sp5 = db.ChiTietSanPhams.Where(x => x.MaSP == 45 || x.MaSP == 46 || x.MaSP == 48 || x.MaSP == 49 || x.MaSP == 50 || x.MaSP == 55 || x.MaSP == 56 || x.MaSP == 57 || x.MaSP == 59 || x.MaSP == 61).Count();
            //int sp6 = db.ChiTietSanPhams.Where(x => x.MaSP == 71).Count();
            //int sp16 = db.ChiTietSanPhams.Where(x => x.MaSP == 72).Count();
            
            Ratio obj = new Ratio();
            obj.sp1 = sp1;
            obj.sp3 = sp3;
            obj.sp4 = sp4;
            obj.sp5 = sp5;
            //obj.sp6 = sp6;
            //obj.sp16 = sp16;

            return Json(obj, JsonRequestBehavior.AllowGet);
            //return View();

        }
        public class Ratio
        {
            public int sp1 { get; set; }
            public int sp5 { get; set; }
            public int sp3 { get; set; }
            public int sp4 { get; set; }
            public int sp6 { get; set; }
            public int sp16 { get; set; }
        }
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public static object[] GetChartData()
        //{
        //    List<SanPham> data = new List<SanPham>();
        //    //Here MyDatabaseEntities  is our dbContext
        //    using (QLBanXeEntities dc = new QLBanXeEntities())
        //    {
        //        data = dc.SanPhams.ToList();
        //    }

        //    var chartData = new object[data.Count + 1];
        //    chartData[0] = new object[]{
        //        "Năm",
        //        "MaLoai",
        //        "MaNhaCC"
        //    };

        //    int j = 0;
        //    foreach (var i in data)
        //    {
        //        j++;
        //        chartData[j] = new object[] { i.NgayCapNhat.ToString(), i.MaLoai, i.MaNCC };
        //    }
        //    return chartData;
        //}

    }
}