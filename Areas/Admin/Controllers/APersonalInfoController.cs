using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TraCuuThongTIn.App_Start;
using TraCuuThongTIn.Areas.Admin.AModel;
using TraCuuThongTIn.Areas.Admin.Model;
using TraCuuThongTIn.DBContext;

namespace TraCuuThongTIn.Areas.Admin.Controllers
{
    public class APersonalInfoController : Controller
    {
        private QLMauEntities db = new QLMauEntities();
        #region -- Xử Lý File Upload
        #region -- Upload
        private string pathFile = "/files/promotions/" + DateTime.Now.Year + "/files/" + DateTime.Now.Month + "/";
        private string fileName = "";
        public string UploadImage(HttpPostedFileBase upload)
        {
            fileName = Path.GetFileName(upload.FileName);
            fileName = Processing.UrlImages(fileName);
            bool exsits = System.IO.Directory.Exists(Server.MapPath(pathFile));
            if (!exsits)
                System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));
            var path = Path.Combine(Server.MapPath(pathFile), fileName);
            upload.SaveAs(path);
            return pathFile + fileName;
        }
        #endregion
        #endregion
        public string ReturnDay(int? day)
        {
            string htmlday = "<option value=''>--- Chọn ngày ---</option>";
            for (int i = 1; i <= 31; i++)
            {
                if (day == i)
                {
                    htmlday += $"<option selected value='{i}'>{i}</option>";
                }
                else
                {
                    htmlday += $"<option value='{i}'>{i}</option>";
                }
            }
            return htmlday;
        }

        public string ReturnMonth(int? month)
        {
            string htmlmonth = "<option value=''>--- Chọn tháng ---</option>";
            for (int i = 1; i <= 12; i++)
            {
                if (month == i)
                {
                    htmlmonth += $"<option selected value='{i}'>{i}</option>";
                }
                else
                {
                    htmlmonth += $"<option value='{i}'>{i}</option>";
                }
            }
            return htmlmonth;
        }

        public string ReturnYear(int? year)
        {
            string htmlYear = "<option value=''>--- Chọn năm ---</option>";
            int currentYear = DateTime.Now.Year;
            for (int i = 1990; i <= currentYear + 1; i++)
            {
                if (year == i)
                {
                    htmlYear += $"<option selected value='{i}'>{i}</option>";
                }
                else
                {
                    htmlYear += $"<option value='{i}'>{i}</option>";
                }
            }
            return htmlYear;
        }
        public String GetJobID(int? id)
        {
            String html = "";
            List<AModelJob> lst = new List<AModelJob>();
            lst = (from a in db.tbNgheNghieps
                   where a.Hide != true
                   select (new AModelJob
                   {
                       Id = a.IDNgheNghiep,
                       Name = a.TenNgheNghiep,
                   })).ToList();
            int tong = lst.Count;
            if (id != null)
            {
                html = "<option value=''>----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                }
            }
            else
            {
                html = "<option selected value = ''>-----Chọn-----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value = '" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                }
            }
            return html;
        }
        public String GetJobName(int id)
        {
            try
            {
                return db.tbNgheNghieps.Find(id).TenNgheNghiep;
            }
            catch
            {
                return "";
            }
        }
        public String GetBloodGrID(int? id)
        {
            String html = "";
            List<AModelBloodGr> lst = new List<AModelBloodGr>();
            lst = (from a in db.tbNhomMaus
                   where a.Hide != true
                   select (new AModelBloodGr
                   {
                       Id = a.IDNhomMau,
                       Name = a.TenNhomMau,
                   })).ToList();
            int tong = lst.Count;
            if (id != null)
            {
                html = "<option value=''>----- Chọn -----</option>";
                for (int i = 0; i < tong; i++)
                {
                    if (id == lst[i].Id)
                    {
                        html += "<option selected value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                    else
                    {
                        html += "<option value='" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                    }
                }
            }
            else
            {
                html = "<option selected value = ''>-----Chọn-----</option>";
                for (int i = 0; i < tong; i++)
                {
                    html += "<option value = '" + lst[i].Id + "'>" + lst[i].Name + "</option>";
                }
            }
            return html;
        }
        public String GetBloodGrName(int id)
        {
            try
            {
                return db.tbNhomMaus.Find(id).TenNhomMau;
            }
            catch
            {
                return "";
            }
        }
        // GET: Admin/APersonalInfo
        public ActionResult Index()
        {
            return View(db.tbThongTinCaNhans.ToList());
        }

        // GET: Admin/APersonalInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbThongTinCaNhan tbThongTinCaNhan = db.tbThongTinCaNhans.Find(id);
            if (tbThongTinCaNhan == null)
            {
                return HttpNotFound();
            }
            return View(tbThongTinCaNhan);
        }

        // GET: Admin/APersonalInfo/Create
        public ActionResult Create()
        {
            tbThongTinCaNhan item = new tbThongTinCaNhan();
            return View(item);
        }

        // POST: Admin/APersonalInfo/Create
       
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbThongTinCaNhan tbThongTinCaNhan, HttpPostedFileBase HinhAnh, int day, int month, int year, int bday, int bmonth, int byear)
        {
            tbThongTinCaNhan item = new tbThongTinCaNhan();
            DateTime date = new DateTime(year, month, day);
            DateTime bdate = new DateTime(byear, bmonth, bday);
            try
            {
                if (HinhAnh != null)
                {
                    item.HinhAnh = UploadImage(HinhAnh);
                }
                item.HoTen = tbThongTinCaNhan.HoTen;
                item.SDT = tbThongTinCaNhan.SDT;
                item.Gmail = tbThongTinCaNhan.Gmail;
                item.MatKhau = tbThongTinCaNhan.MatKhau;
                item.IDThanhPho = tbThongTinCaNhan.IDThanhPho;
                item.IDQuan = tbThongTinCaNhan.IDQuan;
                item.IDPhuong = tbThongTinCaNhan.IDPhuong;
                item.NgaySinh = bdate;
                item.GioiTinh = tbThongTinCaNhan.GioiTinh;
                item.CCCD = tbThongTinCaNhan.CCCD;
                item.NgayCap = date;
                item.NoiCap_IDTP = tbThongTinCaNhan.NoiCap_IDTP;
                item.TinhTrangHonNhan = tbThongTinCaNhan.TinhTrangHonNhan;
                item.NgheNghiep = tbThongTinCaNhan.NgheNghiep;
                item.IDNhomMau = tbThongTinCaNhan.IDNhomMau;
                db.tbThongTinCaNhans.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(tbThongTinCaNhan);
            }
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

        // GET: Admin/APersonalInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbThongTinCaNhan tbThongTinCaNhan = db.tbThongTinCaNhans.Find(id);
            if (tbThongTinCaNhan == null)
            {
                return HttpNotFound();
            }
            return View(tbThongTinCaNhan);
        }

        // POST: Admin/APersonalInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDThongTin,HoTen,SDT,Gmail,MatKhau,DiaChi,IDPhuong,IDQuan,IDThanhPho,NgaySinh,GioiTinh,CCCD,NgayCap,NoiCap_IDTP,HinhAnh,TinhTrangHonNhan,NgheNghiep,IDNhomMau,Hide")] tbThongTinCaNhan tbThongTinCaNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbThongTinCaNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbThongTinCaNhan);
        }

        // GET: Admin/APersonalInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbThongTinCaNhan tbThongTinCaNhan = db.tbThongTinCaNhans.Find(id);
            if (tbThongTinCaNhan == null)
            {
                return HttpNotFound();
            }
            return View(tbThongTinCaNhan);
        }

        // POST: Admin/APersonalInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbThongTinCaNhan tbThongTinCaNhan = db.tbThongTinCaNhans.Find(id);
            db.tbThongTinCaNhans.Remove(tbThongTinCaNhan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
