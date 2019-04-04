using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using ClosedXML.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Text;

namespace QuanLyXe.Areas.Admin.Controllers
{
    [Models.AuthorizeNghiepVu]
    public class SanPhamsController : Controller
    {
        private QLBanXeEntities3 db = new QLBanXeEntities3();

        //public ActionResult Index(int? page)
        //{
        //    int pageSize = 10;
        //    // tạo biến số trang
        //    int pageNumber = (page ?? 1);
        //    return View(db.SanPhams.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));

        //}
        public ActionResult Index()
        {

            return View(db.SanPhams.ToList());

        }
        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            List<SanPham> StuList = new List<SanPham>();
            if (SearchBy == "ID")
            {
                try
                {
                    int Id = Convert.ToInt32(SearchValue);
                    StuList = db.SanPhams.Where(x => x.MaSP == Id || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} Is Not A ID ", SearchValue);
                }
                return Json(StuList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                StuList = db.SanPhams.Where(x => x.TenSP.StartsWith(SearchValue) || SearchValue == null).ToList();
                return Json(StuList, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public FileResult Export()
        {

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[11] { new DataColumn("Mã Hàng"),
                                            new DataColumn("Tên Hàng"),
                                            new DataColumn("Ngày Cập Nhật"),
                                            new DataColumn("Hình Ảnh"),
                                            new DataColumn("Mô Tả"),
                                            new DataColumn("Giá Bán"),
                                            new DataColumn("Số Lượng Tồn"),
                                            new DataColumn("Sản Phẩm Mới"),
                                            new DataColumn("Mã Loại"),
                                            new DataColumn("Mã Nhà Cung Cấp"),
                                            new DataColumn("Thanh Toán")


            });

            var sanphams = from sanpham in db.SanPhams.ToList()
                           select sanpham;

            foreach (var sanpham in sanphams)
            {
                dt.Rows.Add(sanpham.MaSP, sanpham.TenSP, sanpham.NgayCapNhat, sanpham.HinhAnh, sanpham.MoTa, sanpham.GiaBan, sanpham.SoLuongTon, sanpham.SanPhamMoi, sanpham.MaLoai, sanpham.MaNCC, sanpham.ThanhToanTT);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
        //    [AcceptVerbs(HttpVerbs.Post)]
        //    public string Import(HttpPostedFileBase uploadFile)
        //    {
        //        StringBuilder strValidations = new StringBuilder(string.Empty);
        //        try
        //        {
        //            if (uploadFile.ContentLength > 0)
        //            {
        //                string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
        //               Path.GetFileName(uploadFile.FileName));
        //                uploadFile.SaveAs(filePath);
        //                DataSet ds = new DataSet();

        //                //A 32-bit provider which enables the use of

        //                string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;";

        //                using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
        //                {
        //                    conn.Open();
        //                    using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
        //                    {
        //                        string sheetName = dtExcelSchema.Rows[0]["SanPham"].ToString();
        //                        string query = "SELECT * FROM [" + sheetName + "]";
        //                        OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
        //                        //DataSet ds = new DataSet();
        //                        adapter.Fill(ds, "Items");
        //                        if (ds.Tables.Count > 0)
        //                        {
        //                            if (ds.Tables[0].Rows.Count > 0)
        //                            {
        //                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                                {
        //                                    //Now we can insert this data to database...
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex) { }
        //        return "";
        //    }
        //}
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Vui lòng chọn 1 file excel";
                return View("Index");
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string fileName = Path.GetFileName(excelfile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content"), fileName);
                    if (System.IO.File.Exists(path))

                    System.IO.File.Delete(path);
                    excelfile.SaveAs(path);
                    // read data from excel file
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<Models.Product> listproducts = new List<Models.Product>();
                    for (int row = 8; row <= range.Rows.Count; row++)
                    {

                        Models.Product p = new Models.Product();
                        p.MaSP = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                        p.TenSP = ((Excel.Range)range.Cells[row, 2]).Text;
                        //p.NgayCapNhat = ((Excel.Range)range.Cells[row, 3]).Text;
                        p.HinhAnh = ((Excel.Range)range.Cells[row, 3]).Text;
                        p.MoTa = ((Excel.Range)range.Cells[row, 4]).Text;
                        p.GiaBan = decimal.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                        p.SoLuongTon = int.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                        p.SanPhamMoi = int.Parse(((Excel.Range)range.Cells[row, 7]).Text);
                        p.MaLoai = int.Parse(((Excel.Range)range.Cells[row, 8]).Text);
                        p.MaNCC = ((Excel.Range)range.Cells[row, 9]).Text;
                        listproducts.Add(p);

                    }
                    ViewBag.ListProducts = listproducts;
                    workbook.Close(path);
                    return View("ImportSuccess");
                }
                else
                {
                    ViewBag.Error = "File type is incorrect<br>";
                    return View("Index");
                }
            }

        }
        //public ActionResult ImportSuccess()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SanPham sanpham, HttpPostedFileBase fileUpload)
        {
            //luu ten file

            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC");
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn Hình Ảnh";
                return View();
            }
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu duong dan cua file
                var path = Path.Combine(Server.MapPath("~/HinhAnhSanPham"), fileName);
                //kiem tra hinh anh da ton tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình Ảnh Đã Tồn Tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sanpham.HinhAnh = fileName;
                db.SanPhams.Add(sanpham);
                db.SaveChanges();
            }
            return View();
        }
        //chỉnh sửa

        [HttpGet]

        public ActionResult Edit(int MaSP)
        {

            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "TenLoai", sanpham.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC", sanpham.MaNCC);
            return View(sanpham);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SanPham sanpham, FormCollection f)
        {
            //SanPham sanpham1 = db.SanPhams.SingleOrDefault(n=>n.MaSP==sanpham.MaSP);
            //sanpham1.MoTa = sanpham.MoTa;
            //sanpham1.MoTa = f.Get("abc").ToString();
            //sanpham1.MoTa = f["abc"].ToString();
            //db.SaveChanges();
            if (ModelState.IsValid)
            {
                // thực hiện cập nhật trong model
                db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.ToList(), "MaLoai", "TenLoai", sanpham.MaLoai);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList(), "MaNCC", "TenNCC", sanpham.MaNCC);
            return View(sanpham);
        }
        public ActionResult Details(int MaSP)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sanpham);
        }
        [HttpGet]
        public ActionResult Delete(int MaSP)
        {
            //Lấy ra đối tượng sách theo mã 
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sanpham);
        }
        [HttpPost, ActionName("Delete")]

        public ActionResult XacNhanXoa(int MaSP)
        {
            SanPham sach = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SanPhams.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: Admin/SanPhams
        //public ActionResult Index()
        //{
        //    var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhaCungCap);
        //    return View(sanPhams.ToList());
        //}

        //// GET: Admin/SanPhams/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sanPham);
        //}

        //// GET: Admin/SanPhams/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai");
        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
        //    return View();
        //}

        //// POST: Admin/SanPhams/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaSP,TenSP,NgayCapNhat,HinhAnh,MoTa,GiaBan,SoLuongTon,SanPhamMoi,MaLoai,MaNCC,ThanhToanTT")] SanPham sanPham)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SanPhams.Add(sanPham);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.MaLoai);
        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
        //    return View(sanPham);
        //}

        //// GET: Admin/SanPhams/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.MaLoai);
        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
        //    return View(sanPham);
        //}

        //// POST: Admin/SanPhams/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaSP,TenSP,NgayCapNhat,HinhAnh,MoTa,GiaBan,SoLuongTon,SanPhamMoi,MaLoai,MaNCC,ThanhToanTT")] SanPham sanPham)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sanPham).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaLoai = new SelectList(db.LoaiSanPhams, "MaLoai", "TenLoai", sanPham.MaLoai);
        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
        //    return View(sanPham);
        //}

        //// GET: Admin/SanPhams/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    if (sanPham == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sanPham);
        //}

        //// POST: Admin/SanPhams/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SanPham sanPham = db.SanPhams.Find(id);
        //    db.SanPhams.Remove(sanPham);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
