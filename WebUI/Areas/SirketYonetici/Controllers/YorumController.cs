using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    [Area("SirketYonetici")]
    public class YorumController : Controller
    {
        private readonly KolayIkContext _context;
        private readonly EfKullaniciYorumuDal _efKullaniciYorumuDal;
        public YorumController(KolayIkContext context)
        {
            _context = context;
            _efKullaniciYorumuDal = new EfKullaniciYorumuDal(context);
        }
        public IActionResult YorumEkle()
        {
            return View(new KullaniciYorumu());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YorumEkle(KullaniciYorumu yorum)
        {
            yorum.CalisanId = KullaniciId();
            yorum.CreatedDate = DateTime.Now;
            yorum.OkunduMu = false;
            bool result=_efKullaniciYorumuDal.YorumKontrol(yorum);
            if (result)
            {
                KullaniciYorumFactory.Factory.AddYorum(yorum);
                return RedirectToAction("Index","Home");
            }
            ViewBag.YorumTanimiHata = "Daha önce " + yorum.YorumTanimi + " bir yorum yaptınız.";
            return View(yorum);
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
