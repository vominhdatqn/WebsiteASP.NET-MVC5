using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// using 2 thu vien thiet ke metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuanLyXe.Areas.Admin.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetadata
        {
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mã Hàng")]
            public int MaSP { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Tên Hàng")]
            public string TenSP { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Ngày Cập Nhật")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Hình Ảnh")]
            public string HinhAnh { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mô Tả")]
            public string MoTa { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]

            [Display(Name = "Giá Bán")]
            public Nullable<decimal> GiaBan { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Số Lượng Tồn")]
            public Nullable<int> SoLuongTon { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mới")]
            public Nullable<int> SanPhamMoi { get; set; }
          
            [Display(Name = "Loại")]
            public string MaLoai { get; set; }
          
            [Display(Name = "Nhà Cung Cấp")]
            public string MaNCC { get; set; }
           
        }
    }
}