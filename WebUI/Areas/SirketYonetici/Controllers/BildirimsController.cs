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
using System.Net.Http;
using Entities.Concrete.Enums;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    [Area("SirketYonetici")]
    public class BildirimsController : Controller
    {
        private readonly KolayIkContext _context;
        private readonly EfBildirimDal _efBildirimDal;
        public BildirimsController(KolayIkContext context)
        {
            //Yeni
            _context = context;
            _efBildirimDal = new EfBildirimDal(context);
        }

        // GET: SirketYonetici/Bildirims
        public async Task<IActionResult> Index()
        {
            var kolayIkContext = _context.Bildirimler.Include(b => b.Calisan);
            return View(await kolayIkContext.ToListAsync());
        }

        // GET: SirketYonetici/Bildirims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bildirim = await _context.Bildirimler
                .Include(b => b.Calisan)
                .FirstOrDefaultAsync(m => m.BildirimId == id);
            if (bildirim == null)
            {
                return NotFound();
            }

            return View(bildirim);
        }

        // GET: SirketYonetici/Bildirims/Create
        public IActionResult Create()
        {
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi");
            return View();
        }

        // POST: SirketYonetici/Bildirims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BildirimId,Baslik,Icerik,CalisanId,OkunduMu,OkunmaTarihi,SablonTanimi,GonderilecekMi,CreatedDate,ModifiedDate")] Bildirim bildirim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bildirim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", bildirim.CalisanId);
            return View(bildirim);
        }

        // GET: SirketYonetici/Bildirims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bildirim = await _context.Bildirimler.FindAsync(id);
            if (bildirim == null)
            {
                return NotFound();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", bildirim.CalisanId);
            return View(bildirim);
        }

        // POST: SirketYonetici/Bildirims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BildirimId,Baslik,Icerik,CalisanId,OkunduMu,OkunmaTarihi,SablonTanimi,GonderilecekMi,CreatedDate,ModifiedDate")] Bildirim bildirim)
        {
            if (id != bildirim.BildirimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bildirim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BildirimExists(bildirim.BildirimId))
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
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", bildirim.CalisanId);
            return View(bildirim);
        }

        // GET: SirketYonetici/Bildirims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bildirim = await _context.Bildirimler
                .Include(b => b.Calisan)
                .FirstOrDefaultAsync(m => m.BildirimId == id);
            if (bildirim == null)
            {
                return NotFound();
            }

            return View(bildirim);
        }

        // POST: SirketYonetici/Bildirims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bildirim = await _context.Bildirimler.FindAsync(id);
            _context.Bildirimler.Remove(bildirim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BildirimExists(int id)
        {
            return _context.Bildirimler.Any(e => e.BildirimId == id);
        }





        ////////////////////7//DUYURU VE ŞABLON BÖLÜMÜ/////////////////////////
        public IActionResult DuyuruGonder()
        {
            return View(new Bildirim());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DuyuruGonder(Bildirim bildirim)
        {
            int id = KullaniciId();
            bildirim.CalisanId = id;
            bildirim.CreatedDate = DateTime.Now;
            bildirim.SablonTanimi = SablonTanimi.Duyuru;
            bildirim.SablonVsBildirim = SablonVsBildirim.Bildirim;

            IEnumerable<Calisan> calisans = CalisanlariGetir();
            if (bildirim.Baslik != null && bildirim.Icerik != null)
            {
                BildirimFactory.Factory.AddBildirim(bildirim);
                foreach (var item in calisans)
                {
                    Bildirim bildirims = new Bildirim
                    {
                        CreatedDate = DateTime.Now,
                        CalisanId = item.CalisanId,
                        OkunduMu = false,
                        Icerik = bildirim.Icerik,
                        Baslik = bildirim.Baslik,
                        SablonTanimi = SablonTanimi.Duyuru,
                        GonderilecekMi = false,
                        SablonVsBildirim = SablonVsBildirim.Bildirim
                    };
                    BildirimFactory.Factory.AddBildirim(bildirims);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(bildirim);

        }


        public ActionResult SablonIndex()//Sablon listesi
        {
            BildirimFactory.Factory.GetBildirimler(out IEnumerable<Bildirim> bildirimler);
            bildirimler = bildirimler.Where(a => a.SablonTanimi != null && a.CalisanId == KullaniciId() && a.SablonVsBildirim == SablonVsBildirim.Sablon);//Yoneticinin oluşturuduğu şablanlar için
            return View(bildirimler);
        }

        public ActionResult SablonDetails(int id)
        {
            BildirimFactory.Factory.GetBildirimById(id, out Bildirim bildirim);
            return View(bildirim);
        }

        public ActionResult SablonCreate()
        {
            int id = KullaniciId();
            Bildirim bildirim = new Bildirim()
            {
                CalisanId = id
            };
            return View(bildirim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SablonCreate(Bildirim bildirim)
        {
            int id = KullaniciId();
            bildirim.CalisanId = id;
            bildirim.SablonVsBildirim = SablonVsBildirim.Sablon;
            if (ModelState.IsValid)
            {
                if (!_efBildirimDal.SablonAktifliginiKarsilastir(bildirim))
                {
                    ViewBag.AktifligiDegistir = "Aynı türde birden fazla şablonu aktif edemezsiniz. Lütfen seçtiğiniz türü değiştirin veya şablonlarınızın aktifliğini değiştirin.";
                    return View(bildirim);
                }
                BildirimFactory.Factory.AddBildirim(bildirim);
                return RedirectToAction("Index", "Home");

            }
            return View();

        }











        private int SirketId()
        {
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(KullaniciId(), out Calisan calisan);
            return (int)calisan.SirketId;
        }

        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }

        private Calisan CalisanGetir(int id)
        {
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(id, out Calisan calisan);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return calisan;
        }
        private IEnumerable<Calisan> CalisanlariGetir()
        {
            int id = KullaniciId();
            if (id == 0) return Enumerable.Empty<Calisan>();
            Calisan calisan = CalisanGetir(id);
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanlar(out IEnumerable<Calisan> Calisanlar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            Calisanlar = Calisanlar.Where(a => a.SirketId == calisan.SirketId && a.AktifMi && a.ErisimTuru == ErisimTuru.Calisan);
            return Calisanlar;
        }
    }
}

