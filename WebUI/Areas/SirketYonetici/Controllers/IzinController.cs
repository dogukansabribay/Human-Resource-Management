using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using WebTestUI.Factories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    [Area("SirketYonetici")]
    public class IzinController : Controller
    {
        private readonly KolayIkContext _context;
        private readonly EfIzinDal _efIzinDal;
        private readonly IWebHostEnvironment Environment;

        public IzinController(KolayIkContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
            _efIzinDal = new EfIzinDal(_context);

        }
        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }
        public ActionResult IzinGoruntulemePage()
        {
            int id = int.Parse(Request.Cookies["UserId"]);
            List<Izin> list = _efIzinDal.GetAll(a => a.CalisanId == id);
            return View(list);
        }
        // GET: SirketYonetici/Izin
        public async Task<IActionResult> Index()
        {
            var kolayIkContext = _context.Izinler.Include(i => i.Calisan).Where(a => a.Calisan.SirketId == SirketId());
            return View(await kolayIkContext.ToListAsync());
        }

        // GET: SirketYonetici/Izin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izin = await _context.Izinler
                .Include(i => i.Calisan)
                .FirstOrDefaultAsync(m => m.IzinID == id);
            if (izin == null)
            {
                return NotFound();
            }

            return View(izin);
        }

        // GET: SirketYonetici/Izin/Create
        public IActionResult Create()
        {
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi");
            return View();
        }

        // POST: SirketYonetici/Izin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IzinID,IzinTanimi,BaslangicTarihi,MesaiBaslangicTarihi,OnayRedDurumu,IzinDetayTalebi,IzinBelgesiYolu,IzinBelgesi,IzinKullanilanGunSayisi,CalisanId,CreatedDate,ModifiedDate")] Izin izin)
        {
            int id = KullaniciId();
            izin.CalisanId = id;
            //Calisan calisan = _efCalisanDal.GetById(id);
            CalisanFactory.Factory.GetCalisanById(id, out Calisan calisan);
            //izin.Calisan = calisan;

            if (!_efIzinDal.IzinBaslangicTarihiBugundenOnceMi(izin))
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
                    IzinFactory.Factory.AddIzin(izin);
                    //_efIzinDal.Add(izin);
                    return RedirectToAction("Index", "Home");

                }
                return View();
            }
        }

        // GET: SirketYonetici/Izin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izin = await _context.Izinler.FindAsync(id);
            if (izin == null)
            {
                return NotFound();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", izin.CalisanId);
            return View(izin);
        }

        // POST: SirketYonetici/Izin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IzinID,IzinTanimi,BaslangicTarihi,MesaiBaslangicTarihi,OnayRedDurumu,IzinDetayTalebi,IzinBelgesiYolu,IzinKullanilanGunSayisi,CalisanId,CreatedDate,ModifiedDate")] Izin izin)
        {
            if (id != izin.IzinID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(izin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IzinExists(izin.IzinID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", izin.CalisanId);
            return View(izin);
        }

        // GET: SirketYonetici/Izin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izin = await _context.Izinler
                .Include(i => i.Calisan)
                .FirstOrDefaultAsync(m => m.IzinID == id);
            if (izin == null)
            {
                return NotFound();
            }

            return View(izin);
        }

        // POST: SirketYonetici/Izin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var izin = await _context.Izinler.FindAsync(id);
            _context.Izinler.Remove(izin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IzinExists(int id)
        {
            return _context.Izinler.Any(e => e.IzinID == id);
        }


        //web apiye geçiririz 

        public IActionResult IzinOnay(int? id)
        {
            var izin = _context.Izinler.Where(a => a.IzinID == id).FirstOrDefault();

            OnaylananIzin(izin);
            return RedirectToAction("index", "Izin");
        }

        void OnaylananIzin(Izin ızin)
        {
            ızin.OnayDurumu = Entities.Concrete.Enums.OnayDurumu.Onaylı;

            _context.Update(ızin);
            _context.SaveChanges();
        }
        //düzenlenmesi lazımmm(dogukan)
        public IActionResult IzinRed(int id, string RedAciklama)
        {
            var izin = _context.Izinler.Where(a => a.IzinID == id).FirstOrDefault();
       
            izin.RedAcıklaması = RedAciklama;
            ReddedilenIzin(izin);


            Bildirim bildirim = new Bildirim();
            bildirim.Baslik = "İzin talebiniz Reddedildi";

            bildirim.CalisanId = izin.CalisanId;

            bildirim.Icerik = izin.BaslangicTarihi +"-"+ izin.MesaiBaslangicTarihi + "tarihleri arasındaki izin talebiniz reddedilmiştir.Sebebi : " + RedAciklama ;
            bildirim.SablonVsBildirim = SablonVsBildirim.Bildirim;
            bildirim.GonderilecekMi = false;
            bildirim.GonderilmeTarihi = DateTime.Now;
            BildirimFactory.Factory.AddBildirim(bildirim);



            return RedirectToAction("index", "Izin");
        }
        void ReddedilenIzin(Izin izin)
        {
            izin.OnayDurumu = Entities.Concrete.Enums.OnayDurumu.Red;
            
            _context.Update(izin);
            _context.SaveChanges();

        }
        private int SirketId()
        {
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(KullaniciId(), out Calisan calisan);
            return (int)calisan.SirketId;
        }
        public ActionResult GetBelirsizIzinSayisi()
        {
            IEnumerable<Calisan> calisanlar;
            CalisanFactory.Factory.GetCalisanlar(out calisanlar);
            IEnumerable<Izin> izinler;
            IzinFactory.Factory.GetIzinler(out izinler);
            var myList = izinler.Join(calisanlar,
                a => a.CalisanId,
                ab => ab.CalisanId,
                (a, ab) => new { izin = a, person = ab }).ToList();

            izinler = myList.Where(a => a.person.SirketId == SirketId()).Select(a => a.izin).ToList();
            izinler = izinler.Where(a => a.OnayDurumu == Entities.Concrete.Enums.OnayDurumu.Belirsiz).ToList();
            var count = izinler.Count();
            var data4 = new { count };
            return Json(data4);
        }
    }
}
