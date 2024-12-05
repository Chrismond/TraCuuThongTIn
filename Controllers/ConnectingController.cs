using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using System.Web.Mvc;
using TraCuuThongTIn.DBContext;
using System.Linq;
using System.Data.Entity;
using TraCuuThongTIn.Models;

namespace TraCuuThongTIn.Controllers
{
    public class ConnectingController : Controller
    {
        QLMauEntities db = new QLMauEntities();
        // GET: Connecting
        public ActionResult Index()
        {
            // Đường dẫn tới các file Excel
            string targetFolder = "/Excel/";
            string tinhThanhPhoPath = Path.Combine(Server.MapPath(targetFolder), "TinhThanhPho.xlsx");
            string quanHuyenPath = Path.Combine(Server.MapPath(targetFolder), "QuanHuyen.xlsx");
            string xaPhuongPath = Path.Combine(Server.MapPath(targetFolder), "XaPhuong.xlsx");

            // Đọc dữ liệu từ file TinhThanhPho.xlsx
            //ReadTinhThanhPho(tinhThanhPhoPath);

            // Đọc dữ liệu từ file QuanHuyen.xlsx
            //ReadQuanHuyen(quanHuyenPath);

            //// Đọc dữ liệu từ file XaPhuong.xlsx
            ReadXaPhuong(xaPhuongPath);

            // Truyền ViewModel đến View
            return View();
        }

        // Hàm đọc file TinhThanhPho.xlsx
        private void ReadTinhThanhPho(string filePath)
        {
            using (ExcelPackage pkd = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet ws = pkd.Workbook.Worksheets[1];
                int rCount = ws.Dimension.End.Row;

                for (int i = 2; i <= rCount; i++)
                {
                    try
                    {
                        int idTP = ws.Cells[i, 1].Value == null ? 0 : Convert.ToInt32(ws.Cells[i, 1].Value.ToString());
                        string tenTP = ws.Cells[i, 2].Value == null ? string.Empty : ws.Cells[i, 2].Value.ToString();

                        if (!string.IsNullOrEmpty(tenTP)) // Kiểm tra nếu TenTP có dữ liệu
                        {
                            tbTinhThanhPho item = new tbTinhThanhPho();
                            item.IDTP = idTP;
                            item.TenTP = tenTP;
                            item.Hide = false;
                            db.tbTinhThanhPhoes.Add(item);

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
                db.SaveChanges();
            }
        }

        // Hàm đọc file QuanHuyen.xlsx
        private void ReadQuanHuyen(string filePath)
        {
            using (ExcelPackage pkd = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet ws = pkd.Workbook.Worksheets[1];
                int rCount = ws.Dimension.End.Row;

                for (int i = 2; i <= rCount; i++)
                {
                    try
                    {
                        int idTP = ws.Cells[i, 1].Value == null ? 0 : Convert.ToInt32(ws.Cells[i, 1].Value.ToString());
                        string tenQuan = ws.Cells[i, 2].Value == null ? string.Empty : ws.Cells[i, 2].Value.ToString();
                        int idQuan = ws.Cells[i, 3].Value == null ? 0 : Convert.ToInt32(ws.Cells[i, 3].Value.ToString());

                        if (!string.IsNullOrEmpty(tenQuan)) // Kiểm tra nếu TenQuan có dữ liệu
                        {
                            tbQuanHuyen item = new tbQuanHuyen();
                            item.IDTP = idTP;
                            item.TenQuan = tenQuan;
                            item.IDQuan = idQuan;
                            item.Hide = false;
                            db.tbQuanHuyens.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
                db.SaveChanges();
            }
        }

        // Hàm đọc file XaPhuong.xlsx
        private void ReadXaPhuong(string filePath)
        {
            using (ExcelPackage pkd = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet ws = pkd.Workbook.Worksheets[1];
                int rCount = ws.Dimension.End.Row;

                for (int i = 2; i <= rCount; i++)
                {
                    try
                    {
                        int idQuan = ws.Cells[i, 1].Value == null ? 0 : Convert.ToInt32(ws.Cells[i, 1].Value.ToString());
                        string tenPhuong = ws.Cells[i, 2].Value == null ? string.Empty : ws.Cells[i, 2].Value.ToString();
                        int idPhuong = ws.Cells[i, 3].Value == null ? 0 : Convert.ToInt32(ws.Cells[i, 3].Value.ToString());

                        if (!string.IsNullOrEmpty(tenPhuong)) // Kiểm tra nếu TenPhuong có dữ liệu
                        {
                            tbXaPhuong item = new tbXaPhuong();
                            item.IDPhuong = idPhuong;
                            item.TenPhuong = tenPhuong;
                            item.IDQuan = idQuan;
                            item.Hide = false;
                            db.tbXaPhuongs.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
                db.SaveChanges();
            }
        }
        



    }

    // Các model lưu trữ dữ liệu
    public class TinhThanhPho
    {
        public int IDTP { get; set; }
        public string TenTP { get; set; }
    }

    public class QuanHuyen
    {
        public int IDTP { get; set; }
        public string TenQuan { get; set; }
        public int IDQuan { get; set; }
    }

    public class XaPhuong
    {
        public int IDQuan { get; set; }
        public string TenPhuong { get; set; }
        public int IDPhuong { get; set; }
    }

    // ViewModel để truyền dữ liệu đến View
    public class MyViewModel
    {
        public List<TinhThanhPho> TinhThanhPhoList { get; set; }
        public List<QuanHuyen> QuanHuyenList { get; set; }
        public List<XaPhuong> XaPhuongList { get; set; }
    }
}
