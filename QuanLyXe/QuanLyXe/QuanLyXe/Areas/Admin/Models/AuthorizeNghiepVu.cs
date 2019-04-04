using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
namespace QuanLyXe.Areas.Admin.Models
{
    public class AuthorizeNghiepVu : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["MaKH"]==null)
            {
                filterContext.Result = new RedirectResult("/Admin/Home/Login");
                return;
            }
            int userID = int.Parse(HttpContext.Current.Session["MaKH"].ToString());
            string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" + filterContext.ActionDescriptor.ActionName;
            QLBanXeEntities3 db = new QLBanXeEntities3();
            var admin = db.KhachHangs.Where(a=>a.MaKH==userID && a.isAdmin.Value!=0).FirstOrDefault();
            if (admin!=null)
                return;
            var listpermission = from p in db.BlogPermissions
                                 join g in db.CapPheps on p.PermissionId equals g.PermissionId
                                 where g.MaKH == userID
                                 select p.PermisstionName;
            if (!listpermission.Contains(actionName))
            {
                filterContext.Result = new RedirectResult("/Admin/Home/NotificationAuthorize");
                return;
            }
            

        }
    }
}