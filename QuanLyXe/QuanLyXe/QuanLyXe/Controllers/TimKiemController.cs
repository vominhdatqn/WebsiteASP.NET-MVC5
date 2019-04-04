using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using PagedList;
using PagedList.Mvc;
using System.Text.RegularExpressions;

namespace QuanLyXe.Controllers
{
    public class TimKiemController : Controller
    {
        QLBanXeEntities3 db = new QLBanXeEntities3();
        // GET: TimKiem
     
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            List<SanPham> lstKQTK;

            if (regex.IsMatch(sTuKhoa))
            {
                double stu = double.Parse(sTuKhoa);
                lstKQTK = db.SanPhams.Where(n => n.GiaBan <= stu&& n.MaLoai==stu.ToString()).ToList();

            }
            else
            {
                lstKQTK = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa) || n.MaNCC.Contains(sTuKhoa) || n.MaLoai.Contains(sTuKhoa)).ToList();
          
            }
         
            // phân trang


            int pageNumber = (page ?? 1);
            int pageSize = 6;

            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào";
                return View(db.SanPhams.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã Tìm Thấy " + lstKQTK.Count + "Kết Quả!";
            return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {


            List<SanPham> lstKQTK = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();
            // phân trang


            int pageNumber = (page ?? 1);
            int pageSize = 6;

            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào";
                return View(db.SanPhams.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã Tìm Thấy " + lstKQTK.Count + "Kết Quả!";
            return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }
       
        //public ActionResult TimTheoGia(FormCollection formCollection)
        //{
        //    if (formCollection["txtTimTheoGiaNho"].ToString() == null || formCollection["txtTimTheoGiaLon"].ToString() == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    double giaTu = double.Parse(formCollection["txtTimTheoGiaNho"].ToString());
        //    decimal giaDen = decimal.Parse(formCollection["txtTimTheoGiaLon"].ToString());
        //    List<SanPham> lstKQ = db.SanPhams.Where(n => n.GiaBan >= giaTu && n.GiaBan <= giaDen).OrderBy(n => n.GiaBan).ToList();
        //    if (lstKQ.Count == 0)
        //    {
        //        ViewBag.ThongBao = "Không tìm thấy kết quả nào!";
        //        return View(lstKQ);
        //    }
        //    ViewBag.ThongBao = "Đã tìm thấy " + lstKQ.Count + " kết quả!";
        //    return View(lstKQ);
        //}
    }
}