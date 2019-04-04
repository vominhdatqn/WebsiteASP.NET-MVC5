using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyXe.Areas.Admin.Models
{
    public class Product
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public Nullable<int> SanPhamMoi { get; set; }
        public string MaLoai { get; set; }
        public string MaNCC { get; set; }
    }
}