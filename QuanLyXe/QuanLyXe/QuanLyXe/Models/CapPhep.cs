//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyXe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CapPhep
    {
        public int PermissionId { get; set; }
        public int MaKH { get; set; }
        public string Description { get; set; }
    
        public virtual BlogPermission BlogPermission { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
