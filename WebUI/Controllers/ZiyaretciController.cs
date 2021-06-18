using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Controllers
{
    public class ZiyaretciController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult YorumOku(int id)
        {
            KullaniciYorumFactory.Factory.GetYorumById(id, out KullaniciYorumu yorum);
            CalisanFactory.Factory.GetCalisanById(yorum.CalisanId, out Calisan calisan);
            SirketFactory.Factory.GetSirketById((int)calisan.SirketId, out Sirket sirket);
            calisan.Sirket = sirket;
            yorum.Calisan = calisan;
            return View(yorum);

        }
    }
}
