using DataAccess.Concrete.EntityFramework;
using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    [Area("SirketYonetici")]
    public class IzinBilgileriYuklemeController : Controller
    {
        private readonly IWebHostEnvironment Environment;
        private readonly EfCalisanDal _calisanDal;

        public IzinBilgileriYuklemeController(IWebHostEnvironment _environment, KolayIkContext context)
        {
            Environment = _environment;
            _calisanDal = new EfCalisanDal(context);
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadFile()
        {
            string path = Path.Combine(this.Environment.WebRootPath + "/Files/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "Calisan.xlsx");
            string fileName = "Calisan.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public IActionResult UploadFile()
        {
            FileUploadViewModel model = new FileUploadViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult UploadFile(FileUploadViewModel model)
        {
            string rootFolder = Environment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + model.XlsFile.FileName;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            using (var stream = new MemoryStream())
            {
                model.XlsFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    package.SaveAs(file);
                }
            }
            //After save excel file in wwwroot and then
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    ViewBag.Hata = "Geçersiz dosya yüklediniz.";
                    return RedirectToAction("Index", "IzinBilgileriYukleme");
                }
                else
                {
                    var rowCount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        model.StaffInfoViewModel = new StaffInfoViewModel();
                        model.StaffInfoViewModel.Ad = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString().Trim();
                        model.StaffInfoViewModel.Soyad = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString().Trim();
                        model.StaffInfoViewModel.EPostaSirket = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString().Trim();
                        model.StaffInfoViewModel.YıllıkIzinHakki = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString().Trim();

                        Calisan calisan = _calisanDal.GetAll(a => a.Adi == model.StaffInfoViewModel.Ad && a.Soyadi == model.StaffInfoViewModel.Soyad && a.MailIs == model.StaffInfoViewModel.EPostaSirket).FirstOrDefault();
                        calisan.KalanYıllıkIzinSayisi = int.Parse(model.StaffInfoViewModel.YıllıkIzinHakki);
                        calisan.KullandigiYıllıkIzinSayisi = 0;
                        calisan.ToplamKullanilanIzinSayisi = 0;
                        CalisanFactory.Factory.Guncelle(calisan.CalisanId, calisan);

                    }

                }
            }
            ViewBag.Basari = "Personellerinizin izin hakları başarıyla yüklendi.";
            return RedirectToAction("Index","IzinBilgileriYukleme") ;
        }


    }
}
