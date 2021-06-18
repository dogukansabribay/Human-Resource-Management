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
using Entities.Concrete.Enums;
using System.Net.Http;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    [Area("SirketYonetici")]
    public class CalisanHarcamaController : Controller
    {
        private readonly KolayIkContext _context;

        public CalisanHarcamaController(KolayIkContext context)
        {
            _context = context;
        }
        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }
        // GET: SirketYonetici/CalisanHarcama
        //[Route("/SirketYonetici/CalisanHarcama/index")]
        public async Task<IActionResult> Index()
        {
            var kolayIkContext = _context.CalisanHarcamalari.Include(c => c.Calisan).Where(a => a.Calisan.SirketId == SirketId());
            return View(await kolayIkContext.ToListAsync());

        }

        [Route("/SirketYonetici/CalisanHarcama/HarcamaOnay/{id}")]
        public IActionResult HarcamaOnay(int? id)
        {
            var harcama = _context.CalisanHarcamalari.Where(a => a.CalisanHarcamaID == id).FirstOrDefault();
            OnaylananHarcama(harcama);
            return RedirectToAction("index", "CalisanHarcama");
        }
        void OnaylananHarcama(CalisanHarcama harcama)
        {
            harcama.OnayDurumu = Entities.Concrete.Enums.OnayDurumu.Onaylı;
            _context.Update(harcama);
            _context.SaveChanges();
        }       
        void ReddedilenHarcama(CalisanHarcama harcama) 
        {
            harcama.OnayDurumu = Entities.Concrete.Enums.OnayDurumu.Red;
            _context.Update(harcama);
            _context.SaveChanges();

        }
        //[Route("/SirketYonetici/CalisanHarcama/HarcamaRed/{int:id}")]
        public IActionResult HarcamaRed(int id , string RedAciklama)
        {
            var harcama = _context.CalisanHarcamalari.Where(a => a.CalisanHarcamaID == id).FirstOrDefault();
            harcama.RedAcıklaması = RedAciklama;
            ReddedilenHarcama(harcama);

            Bildirim bildirim = new Bildirim();
            bildirim.Baslik = "Harcama talebiniz Reddedildi";

            bildirim.CalisanId = harcama.CalisanId;

            bildirim.Icerik = harcama.HarcamaMiktari + " tutarındaki harcama talebiniz " + DateTime.Now + " tarihinde şu sebepten ötürü reddedilmiştir.Sebep: " + RedAciklama;
            bildirim.SablonVsBildirim = SablonVsBildirim.Bildirim;
            bildirim.GonderilecekMi = false;
            bildirim.GonderilmeTarihi = DateTime.Now;
            BildirimFactory.Factory.AddBildirim(bildirim);

            return RedirectToAction("index", "calisanharcama");
        }

        // GET: SirketYonetici/CalisanHarcama/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanHarcama = await _context.CalisanHarcamalari
                .Include(c => c.Calisan)
                .FirstOrDefaultAsync(m => m.CalisanHarcamaID == id);
            if (calisanHarcama == null)
            {
                return NotFound();
            }

            return View(calisanHarcama);
        }


        public ActionResult GetBelirsizHarcamaSayisi()
        {            
            IEnumerable<Calisan> calisanlar;
            CalisanFactory.Factory.GetCalisanlar(out calisanlar);

            IEnumerable<CalisanHarcama> harcamalar;
            CalisanHarcamaFactory.Factory.GetHarcamalar(out harcamalar);
            var myList = harcamalar.Join(calisanlar,
                a => a.CalisanId,
                ab => ab.CalisanId,
                (a, ab) => new { harcama = a, person = ab }).ToList();
            harcamalar = myList.Where(a => a.person.SirketId == SirketId()).Select(a => a.harcama).ToList();
            harcamalar = harcamalar.Where(a => a.OnayDurumu == OnayDurumu.Belirsiz).ToList();
            var count = harcamalar.Count();
            var data3 = new { count };
            return Json(data3);
        }

        private int SirketId()
        {
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(KullaniciId(), out Calisan calisan);
            return (int)calisan.SirketId;
        }
        private bool CalisanHarcamaExists(int id)
        {
            return _context.CalisanHarcamalari.Any(e => e.CalisanHarcamaID == id);
        }
    }
}
