using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyXe.Areas.Admin.Models
{
    [MetadataTypeAttribute(typeof(NghiepVuMetadata))]
    public partial class NghiepVu
    {

        internal sealed class NghiepVuMetadata
        {
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mã Nghiệp Vụ")]
            public string MaNghiepVu { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Tên Nghiệp Vụ")]
            public string TenNghiepVu { get; set; }
        }
    }
}