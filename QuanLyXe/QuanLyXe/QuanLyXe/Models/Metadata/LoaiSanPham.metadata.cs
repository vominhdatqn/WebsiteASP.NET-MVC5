using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyXe.Models
{
    [MetadataTypeAttribute(typeof(LoaiSanPhamMetadata))]
    public partial class LoaiSanPham
    {
        internal sealed class LoaiSanPhamMetadata
        {
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mã Loại Xe")]
            public string MaLoai { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Tên Loại Xe")]
            public string TenLoai { get; set; }
        }
    }
}