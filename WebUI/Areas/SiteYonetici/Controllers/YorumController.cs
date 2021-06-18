using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SiteYonetici.Controllers
{

    [Area("SiteYonetici")]
    public class YorumController : Controller
    {
        private readonly KolayIkContext _context;
        public YorumController(KolayIkContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            IEnumerable<KullaniciYorumu> yorumular;
            KullaniciYorumFactory.Factory.GetYorumlar(out yorumular);
            foreach (var item in yorumular)
            {
                Calisan calisan;
                CalisanFactory.Factory.GetCalisanById(item.CalisanId, out calisan);
                item.Calisan = calisan;
            }
            return View(yorumular);
        }

        public ActionResult GetOkunmamışYorumSayisi()
        {
            IEnumerable<KullaniciYorumu> yorumular;
            KullaniciYorumFactory.Factory.GetYorumlar(out yorumular);            
            yorumular = yorumular.Where(a => a.OkunduMu == false).ToList();
            var count = yorumular.Count();
            var data = new { count };
            return Json(data);
        }
        public IActionResult YorumDetay(int id)
        {
            KullaniciYorumu yorum;
            KullaniciYorumFactory.Factory.GetYorumById(id, out yorum);
            yorum.OkunduMu = true;
            KullaniciYorumFactory.Factory.Guncelle(id, yorum);


            return View(yorum);

        }

    }
}
