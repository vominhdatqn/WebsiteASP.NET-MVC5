using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyXe.Models
{
    [MetadataTypeAttribute(typeof(ChiTietSanPhamMetadata))]
    public partial class ChiTietSanPham
    {
        internal sealed class ChiTietSanPhamMetadata
        {
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mã Xe")]
            public int MaSP { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mã Đơn Hàng")]
            public int MaDonHang { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Số Lượng")]
            public Nullable<int> SoLuong { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Đơn Giá")]
            public Nullable<decimal> DonGia { get; set; }
        }
    }
}