using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using QuanLyXe.Areas.Admin.Models;

namespace QuanLyXe.Areas.Admin.Controllers
{
    [AuthorizeNghiepVu]
    public class CapPhepsController : Controller
    {
        QLBanXeEntities3 db = new QLBanXeEntities3();
        // GET: Admin/CapPhep
        public ActionResult Index(int id)
        {
            var listcontrol = db.NghiepVus.AsEnumerable();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in listcontrol)
            {
                items.Add(new SelectListItem() { Text = item.TenNghiepVu, Value = item.MaNghiepVu });

            }
            // lưu ra biến
            ViewBag.items = items;
            //lưu danh sách quyền đã được cấp
            var listgranted = from g in db.CapPheps
                              join p in db.BlogPermissions on g.PermissionId equals p.PermissionId
                              where g.MaKH == id
                              select new SelectListItem() { Value = p.PermissionId.ToString(), Text = p.Description };
            //lưu ra biến
            ViewBag.listgranted = listgranted;
            // lưu id  của người dùng đang được cấp ra session
            Session["usergrant"] = id;
            //lấy người dùng
            var usergrant = db.KhachHangs.Find(id);
            //lưu tên ra biến
            ViewBag.usergrant = usergrant.TaiKhoan + "(" + usergrant.TenKH + ")";
            return View();
        }

        public JsonResult getPermissions(string id, int usertemp)
        {
            //lấy tất cả các permission của user và của nghiệp vụ
            var listgranted = (from g in db.CapPheps
                             join p in db.BlogPermissions on g.PermissionId equals p.PermissionId
                             where g.MaKH == usertemp && p.MaNghiepVu == id
                             select new PermissionAction { PermissionId = p.PermissionId, PermisstionName = p.PermisstionName, Description = p.Description, IsGranted = true }).ToList();
            //lấy tất cả các permission của nghiệp vụ hiện tại
            var listpermission = from p in db.BlogPermissions
                                 where p.MaNghiepVu == id
                                 select new PermissionAction { PermissionId = p.PermissionId, PermisstionName = p.PermisstionName, Description = p.Description, IsGranted = false };
            //lấy các id của permission đã được gán ở trên cho người dùng
            var listpermissionID = listgranted.Select(p => p.PermissionId);
            //so sánh kiểm xem permissionid nào của nghiệp vụ mà chưa có trong listgrant thì đưa vào {isGrant=false}
            foreach (var item in listpermission)
            {
                if (!listpermissionID.Contains(item.PermissionId))
                    listgranted.Add(item);
            }
            return Json(listgranted.OrderBy(x => x.Description), JsonRequestBehavior.AllowGet);
        }

        public string updatePermission(int id, int usertemp)
        {
            string msg = "";
            var grant = db.CapPheps.Find(id,usertemp);
            if (grant == null)
            {
                CapPhep cp = new CapPhep() { PermissionId = id, MaKH = usertemp, Description = "" };
                db.CapPheps.Add(cp);
                msg = /*"<div class='alert alert-success'>Quyền cấp thành công</div>"*/ "<div class='alert alert-success'><h3 class='h4 mb-0 text-success' style='text-align:center'>Cấp Quyền Thành Công<i class='ar fa-check - circle'></i></h3></div>";
            }
            else
            {
                db.CapPheps.Remove(grant);
                msg = /*"<div class='alert alert-danger'>Quyền hủy thành công</div>";*/ "<div class='alert alert-danger'><h3 class='h4 mb-0 text-danger' style='text-align:center'>Quyền Hủy Thành Công<i class='fas fa-exclamation - triangle'></i></h3></div>";
            }
            db.SaveChanges();
            return msg;

        }
    }
}