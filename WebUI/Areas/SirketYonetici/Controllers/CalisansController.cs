using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System.Net.Http;
using WebTestUI.Factories;
using DataAccess.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Entities.Concrete.Enums;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    [Area("SirketYonetici")]
    public class CalisansController : Controller
    {
        private readonly KolayIkContext _context;
        private readonly EfEgitimBilgiDal efEgitim;
        private readonly EfCalisanHarcamaDal efCalisanHarcama;
        private readonly EfCalisanKariyerDal efCalisanKariyerDal;

        private readonly IWebHostEnvironment Environment;
        public CalisansController(KolayIkContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
            efEgitim = new EfEgitimBilgiDal(context);
            efCalisanHarcama = new EfCalisanHarcamaDal(context);
            efCalisanKariyerDal = new EfCalisanKariyerDal(context);
        }
        public IActionResult CalisanPasif(int id)
        {
            Calisan calisan = CalisanGetir(id);
            calisan.AktifMi = false;
            CalisanFactory.Factory.Guncelle(id, calisan);
            return RedirectToAction("index");
        }
        private Calisan CalisanGetir(int id)
        {
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(id, out Calisan calisan);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return calisan;
        }
        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }
        private IEnumerable<Calisan> CalisanlariGetir()
        {
            int id = KullaniciId();
            if (id == 0) return Enumerable.Empty<Calisan>();
            Calisan calisan = CalisanGetir(id);
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanlar(out IEnumerable<Calisan> Calisanlar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            Calisanlar = Calisanlar.Where(a => a.SirketId == calisan.SirketId && a.AktifMi && a.ErisimTuru==ErisimTuru.Calisan);
            return Calisanlar;
        }
        // GET: SirketYonetici/Calisans
        public IActionResult Index()
        {           
            return View(CalisanlariGetir());
        }
        public IActionResult CalisanGoruntuleme(string ara)
        {            
            IEnumerable<Calisan> calisanlar = CalisanlariGetir().Where(a=>a.SirketId==SirketId());
            if (!string.IsNullOrEmpty(ara))
            {
                calisanlar = calisanlar.Where(x => x.Adi.Contains(ara) || x.Soyadi.Contains(ara)).ToList();
            }
            return View(calisanlar);
        }

        // GET: SirketYonetici/Calisans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar
                .Include(c => c.Sirket)
                .FirstOrDefaultAsync(m => m.CalisanId == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // GET: SirketYonetici/Calisans/Create
        public IActionResult Create()
        {
            //ViewData["SirketId"] = new SelectList(_context.Sirketler, "SirketId", "SirketId");
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("BildirimSayisi");
            return RedirectToAction("index", "home");
        }
        // POST: SirketYonetici/Calisans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CalisanId,Adi,Soyadi,DogumTarihi,TcNo,ErisimTuru,SozlesmeTuru,EngelDerecesi,Uyruk,Cinsiyet,KanGrubu,EgitimDurumu,EgitimSeviyesi,SonTamamlananEgitimKurumu,CocukSayisi,KullandigiYıllıkIzinSayisi,KalanYıllıkIzinSayisi,ToplamKullanilanIzinSayisi,MailIs,MailKisisel,TelIs,TelKisisel,Fotograf,AktifMi,IseGirisTarihi,SozlesmeBitisTarihi,MedeniDurum,SirketId,AcilDurumBilgiID,AdresId,BaglantiSosyalMedyaId,BankaId,CalisanAvansID,KullaniciYorumuId,CreatedDate,ModifiedDate,ResimDosyasi")] Calisan calisan)
        {
            if (!Regex.IsMatch(calisan.TcNo,@"^\d+$"))
            {
                ViewBag.TcNo = "TC no yalnızca sayı içermelidir.";
                return View(calisan);
            }
            IEnumerable<Calisan> calisanlar;
            CalisanFactory.Factory.GetCalisanlar(out calisanlar);
            calisanlar = calisanlar.Where(a => a.SirketId == SirketId()&&a.AktifMi);

            SirketUyelikPlani sirketUyelikPlani;
            IEnumerable<SirketUyelikPlani> planlar;
            SirketUyelikPlaniFactory.Factory.GetPlanlar(out planlar);
            sirketUyelikPlani = planlar.Where(a => a.SirketID == SirketId()).FirstOrDefault();

            UyelikPlani plan;
            UyelikPlaniFactory.Factory.GetUyelikPlaniById(sirketUyelikPlani.UyelikPlaniID,out plan);
            if (calisanlar.Count()>=plan.CalisanSayisi)
            {
                ViewBag.CalisanSayiHatasi = "Limiti aşıyorsunuz. Paketinizi yükseltin.";
                return View(calisan);
            }
            int YoneticiId = KullaniciId();
            CalisanFactory.Factory.GetCalisanById(YoneticiId, out Calisan yonetici);
            calisan.SirketId = yonetici.SirketId;
            calisan.AktifMi = true;
            calisan.ErisimTuru = ErisimTuru.Calisan;
            calisan.SozlesmeTuru = SozlesmeTuru.Suresiz;
            calisan.CreatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                string resimler = Path.Combine(this.Environment.WebRootPath, "PersonelResimler");
                if (calisan.ResimDosyasi != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(resimler, calisan.ResimDosyasi.FileName), FileMode.Create))
                    {
                        calisan.ResimDosyasi.CopyToAsync(fileStream);
                    }
                    calisan.Fotograf = calisan.ResimDosyasi.FileName;
                }
                calisan.ResimDosyasi = null;
                CalisanFactory.Factory.AddCalisan(calisan);
                BildirimMesaji(calisan,yonetici);
                SifreGonder();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SirketId"] = new SelectList(_context.Sirketler, "SirketId", "SirketId", calisan.SirketId);
            return View(calisan);
        }
        private int SirketId()
        {
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(KullaniciId(), out Calisan calisan);
            return (int)calisan.SirketId;
        }
        private static string RandomPassword(int length)
        {
            string allowed = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(allowed
                .OrderBy(o => Guid.NewGuid())
                .Take(length)
                .ToArray());
        }
        private void SifreGonder()
        {
            Random random = new Random();
            Calisan calisan = SonEklenenCalisan();
            Sifre sifre = new Sifre
            {
                OlusturulmaTarihi = DateTime.Now,
                CalisanId = calisan.CalisanId,
                CreatedDate = DateTime.Now,
                Parola = RandomPassword(random.Next(8, 12))
            };
            SifreFactory.Factory.AddSifre(sifre);
            SmtpClient sc = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = new NetworkCredential("dragunov199137@gmail.com", "741852963Ab")
            };
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("dragunov199137@gmail.com")
            };           
            mail.To.Add(calisan.MailIs);
            mail.To.Add(calisan.MailKisisel);
            mail.Subject = "Aramıza hoşgeldiniz.";
            mail.IsBodyHtml = true;
            mail.Body = "Siteye " + calisan.MailIs + " adresiyle ve   "+sifre.Parola + "  şifresiyle giriş yapabilirsiniz.Lütfen güvenliğiniz açısından ilk giriş yaptığınızda şifrenizi değiştiriniz.";
            sc.Send(mail);
        }

        private Calisan SonEklenenCalisan()
        {
            CalisanFactory.Factory.GetCalisanlar(out IEnumerable<Calisan> calisanlar);
            return calisanlar.OrderByDescending(a => a.CreatedDate).FirstOrDefault();
        }
        private void BildirimMesaji(Calisan calisan, Calisan yonetici)
        {

            calisan = SonEklenenCalisan();
            //SablonFactory.Factory.GetSablonlar(out IEnumerable<Sablon> sablonlar);
            //Sablon sablon = sablonlar.Where(a => a.CalisanId == yonetici.CalisanId && a.SablonTanimi==SablonTanimi.Hosgeldin).FirstOrDefault();
            Bildirim bildirim = new Bildirim
            {
                //bildirim.Calisan = calisan;
                CalisanId = calisan.CalisanId,
                //SablonId = sablon.SablonId,
                //bildirim.Sablon = sablon;
                CreatedDate = DateTime.Now,
                OkunduMu = false
            };
            BildirimFactory.Factory.AddBildirim(bildirim);
        }

        // GET: SirketYonetici/Calisans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar.FindAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }
            ViewData["SirketId"] = new SelectList(_context.Sirketler, "SirketId", "SirketId", calisan.SirketId);
            return View(calisan);
        }

        // POST: SirketYonetici/Calisans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalisanId,Adi,Soyadi,DogumTarihi,TcNo,ErisimTuru,SozlesmeTuru,EngelDerecesi,Uyruk,Cinsiyet,KanGrubu,EgitimDurumu,EgitimSeviyesi,SonTamamlananEgitimKurumu,CocukSayisi,KullandigiYıllıkIzinSayisi,KalanYıllıkIzinSayisi,ToplamKullanilanIzinSayisi,MailIs,MailKisisel,TelIs,TelKisisel,Fotograf,AktifMi,IseGirisTarihi,SozlesmeBitisTarihi,MedeniDurum,SirketId,AcilDurumBilgiID,AdresId,BaglantiSosyalMedyaId,BankaId,CalisanAvansID,KullaniciYorumuId,CreatedDate,ModifiedDate,ResimDosyasi")] Calisan calisan)
        {
            if (id != calisan.CalisanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanExists(calisan.CalisanId))
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
            ViewData["SirketId"] = new SelectList(_context.Sirketler, "SirketId", "SirketId", calisan.SirketId);
            return View(calisan);
        }

        // GET: SirketYonetici/Calisans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar
                .Include(c => c.Sirket)
                .FirstOrDefaultAsync(m => m.CalisanId == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // POST: SirketYonetici/Calisans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calisan = await _context.Calisanlar.FindAsync(id);
            _context.Calisanlar.Remove(calisan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanExists(int id)
        {
            return _context.Calisanlar.Any(e => e.CalisanId == id);
        }
    }
}
