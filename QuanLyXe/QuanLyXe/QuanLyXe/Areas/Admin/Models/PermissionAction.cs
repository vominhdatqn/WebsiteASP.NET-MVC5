using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyXe.Areas.Admin.Models
{
    public class PermissionAction
    {
        public int PermissionId { get; set; }
        public string PermisstionName { get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
    }
}