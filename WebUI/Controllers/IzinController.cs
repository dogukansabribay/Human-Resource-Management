using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Controllers
{
    public class IzinController : Controller
    {
        private readonly KolayIkContext _context;
        private readonly IWebHostEnvironment Environment;
        private readonly EfIzinDal _efIzinDal;
        private readonly EfCalisanDal _efCalisanDal;
        public IzinController(KolayIkContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
            _efIzinDal = new EfIzinDal(_context);
            _efCalisanDal = new EfCalisanDal(_context);

        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IzinGorutulemePage()
        {
            int id = int.Parse(Request.Cookies["UserId"]);
            List<Izin> list = _efIzinDal.GetAll(a => a.CalisanId == id);
            return View(list);
        }

        public IActionResult Create()
        {
            int id = int.Parse(Request.Cookies["UserId"]);
            Izin izin = new Izin
            {
                CalisanId = id
            };
            Calisan calisan = _efCalisanDal.GetById(id);
            izin.Calisan = calisan;
            return View(izin);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IzinID,IzinTanimi,BaslangicTarihi,MesaiBaslangicTarihi,OnayRedDurumu,IzinDetayTalebi,IzinBelgesiYolu,IzinBelgesi, CalisanId")] Izin izin)
        {
            int id = KullaniciId();
            izin.CalisanId = id;
            //Calisan calisan = _efCalisanDal.GetById(id);
            CalisanFactory.Factory.GetCalisanById(id, out Calisan calisan);
            izin.Calisan = calisan;

            if (!_efIzinDal.IzinBaslangicTarihiBugundenOnceMi(izin) && izin.IzinTanimi!=Entities.Concrete.Enums.IzinTanimi.Hastalik)
            {
                ViewBag.GecmisIzinAlamazsiniz = "Geçmiş tarihli izin talebi oluşturamazsınız.";
                return View(izin);
            }
            else if (!_efIzinDal.MesaiBaslangicTarihiIzinBaslangicTarihindenOnceMi(izin))
            {
                ViewBag.TarihleriKontrolEdin = "Mesai başlangıç tarihinizi izin tarihinizden önce bir tarih seçemezsiniz.";
                return View(izin);
            }
            else if (!_efIzinDal.MesaiBaslangicHaftasonuVeyaResmiTatilKontol(izin))
            {
                ViewBag.MesaiBaslangicHaftasonuOlamaz = "Mesai başlangıç tarihini hafta sonu veya resmi tatil seçemezsiniz.";
                return View(izin);
            }
            else if (!_efIzinDal.IzinTarihleriKarsilastir(izin))
            {
                ViewBag.AynıTarihliIzin = "Aynı tarihte başka bir izin talebi oluşturamazsınız. Lütfen tarihleri kontrol edin veya izin talebinizi silin.";
                return View(izin);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string resimler = Path.Combine(this.Environment.WebRootPath, "IzinBelgeleri");
                    if (izin.IzinBelgesi != null)
                    {
                        using (var fileStream = new FileStream(Path.Combine(resimler, izin.IzinBelgesi.FileName), FileMode.Create))
                        {
                            await izin.IzinBelgesi.CopyToAsync(fileStream);
                        }
                        izin.IzinBelgesiYolu = izin.IzinBelgesi.FileName;
                    }
                    izin.IzinBelgesi = null;
                    izin.IzinKullanilanGunSayisi = _efIzinDal.KullanilacakgunSayisiHesapla(izin);
                    if (izin.IzinTanimi == 0)//yıllık izin 
                    {
                        if (calisan.KalanYıllıkIzinSayisi < izin.IzinKullanilanGunSayisi)
                        {
                            ViewBag.YetersizIzinGunu = "Yıllık izin talebi oluşturmak için kalan izin hakkınız yetersizdir.";
                            return View(izin);
                        }
                        else
                        {
                            calisan.KullandigiYıllıkIzinSayisi += izin.IzinKullanilanGunSayisi;
                            calisan.KalanYıllıkIzinSayisi -= izin.IzinKullanilanGunSayisi;
                        }
                    }

                    calisan.ToplamKullanilanIzinSayisi += izin.IzinKullanilanGunSayisi;
                    CalisanFactory.Factory.Guncelle(id, calisan);
                    //_efCalisanDal.Update(calisan);
                    izin.Calisan = null;
                    IzinFactory.Factory.AddIzin(izin);
                    //_efIzinDal.Add(izin);
                    return RedirectToAction("Index", "Home");

                }
                return View();
            }
        }
        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }
    }
}
