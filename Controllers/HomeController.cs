using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraCuuThongTIn.DBContext;
using TraCuuThongTIn.Models;
namespace TraCuuThongTIn.Controllers
{
    public class HomeController : Controller
    {
        QLMauEntities db = new QLMauEntities();
        public ActionResult Index()
        {
            List<tbTinhThanhPho> lst = new List<tbTinhThanhPho> ();
            lst = db.tbTinhThanhPhoes.ToList ();
            return View(lst);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public JsonResult GetJson(int id)
        {
            List<tbQuanHuyen> user = new List<tbQuanHuyen> { };
            try
            {
                user = db.tbQuanHuyens.Where(s => s.IDTP == id).OrderByDescending(a => a.TenQuan).ToList();

            }
            catch
            {
                user = new List<tbQuanHuyen> { };
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetJsonXaPhuong(int idQuan)
        {
            List<tbXaPhuong> XP = new List<tbXaPhuong> { };
            try
            {
                XP = db.tbXaPhuongs.Where(s => s.IDQuan == idQuan).OrderByDescending(a => a.TenPhuong).ToList();
            }
            catch
            {
                XP = new List<tbXaPhuong> { };
            }
            return Json(XP, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBloodGroups()
        {
            var nhomMauList = db.tbNhomMaus.Select(x => new { x.IDNhomMau, x.TenNhomMau }).ToList();
            return Json(nhomMauList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(SearchModel searchData)
        {
            // Chuyển đổi các tham số chuỗi thành kiểu int nếu có giá trị
            int? tinhThanhPho = string.IsNullOrEmpty(searchData.tinhThanhPho) ? (int?)null : int.Parse(searchData.tinhThanhPho);
            int? quanHuyen = string.IsNullOrEmpty(searchData.quanHuyen) ? (int?)null : int.Parse(searchData.quanHuyen);
            int? xaPhuong = string.IsNullOrEmpty(searchData.xaPhuong) ? (int?)null : int.Parse(searchData.xaPhuong);
            int? nhomMauCho = string.IsNullOrEmpty(searchData.nhomMauCho) ? (int?)null : int.Parse(searchData.nhomMauCho);
            int? nhomMauNhan = string.IsNullOrEmpty(searchData.nhomMauNhan) ? (int?)null : int.Parse(searchData.nhomMauNhan);

            // Truy vấn bảng thông tin cá nhân
            var tbThongTinCaNhanList = db.tbThongTinCaNhans
                .Where(x =>
                    (!tinhThanhPho.HasValue || x.IDThanhPho == tinhThanhPho) &&
                    (!quanHuyen.HasValue || x.IDQuan == quanHuyen) &&
                    (!xaPhuong.HasValue || x.IDPhuong == xaPhuong) &&
                    (!nhomMauCho.HasValue || x.IDNhomMau == nhomMauCho) &&
                    (!nhomMauNhan.HasValue || x.IDNhomMau == nhomMauNhan) &&
                    (x.Hide == false || x.Hide == null)  // Chỉ hiển thị những thông tin không bị ẩn
                )
                .ToList();

            // Truy vấn các bảng liên kết riêng biệt
            var phuongList = db.tbXaPhuongs.ToList();
            var quanList = db.tbQuanHuyens.ToList();
            var thanhPhoList = db.tbTinhThanhPhoes.ToList();
            var nhomMauList = db.tbNhomMaus.ToList();

            // Kết hợp các dữ liệu đã truy vấn và trả về kết quả
            var results = tbThongTinCaNhanList.Select(x => new
            {
                x.IDThongTin,
                x.HoTen,
                DiaChi = x.DiaChi ??
                         (phuongList.FirstOrDefault(p => p.IDPhuong == x.IDPhuong)?.TenPhuong + ", " ?? "") +
                         (quanList.FirstOrDefault(q => q.IDQuan == x.IDQuan)?.TenQuan + ", " ?? "") +
                         (thanhPhoList.FirstOrDefault(t => t.IDTP == x.IDThanhPho)?.TenTP ?? ""),
                x.Gmail,
                SoDienThoai = x.SDT,
                NgaySinh = x.NgaySinh.HasValue ? x.NgaySinh.Value.ToString("dd/MM/yyyy") : "",
                GioiTinh = x.GioiTinh.HasValue ? (x.GioiTinh.Value ? "Nam" : "Nữ") : "Không xác định",
                NhomMau = nhomMauList.FirstOrDefault(n => n.IDNhomMau == x.IDNhomMau)?.TenNhomMau ?? "",
                x.HinhAnh
            }).ToList();

            return Json(results);
        }
    }
}