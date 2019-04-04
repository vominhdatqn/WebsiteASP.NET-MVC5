using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyXe.Models;
namespace QuanLyXe.Models
{
    public class GioHang
    {
        QLBanXeEntities3 db = new QLBanXeEntities3();
        public int iMaSP { get; set; }
        public string iTenSP { get; set; }
        public string iHinhAnh { get; set; }
        public Double iDonGia { get; set; }
        public int iSoLuong { get; set; }
        public string ThanhToanTT { get; set; }
        public Double ThanhTien
        {
            get { return iSoLuong * iDonGia; }
        }
        // hàm tạo cho giỏ hàng
        public GioHang(int MaSP)
        {
            iMaSP = MaSP;
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == iMaSP);
            iTenSP = sp.TenSP;
            iHinhAnh = sp.HinhAnh;
            iDonGia = double.Parse(sp.GiaBan.ToString());
            iSoLuong = 1;

        }
    }
}