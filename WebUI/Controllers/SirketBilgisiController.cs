using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Controllers
{
    public class SirketBilgisiController : Controller
    {
        public ActionResult GetSirketAdi()
        {
            Calisan calisan = CalisanGetir(KullaniciId());
            Sirket sirket = SirketGetir((int)calisan.SirketId);
            var Ad = sirket.Adi;
            var dataAd = new { Ad };
            return Json(dataAd);
        }
        public ActionResult GetSirketLogo()
        {
            Calisan calisan = CalisanGetir(KullaniciId());
            Sirket sirket = SirketGetir((int)calisan.SirketId);
            var Logo = sirket.Logo;
            var dataLogo = new { Logo };
            return Json(dataLogo);
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
            Calisan calisan;
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(id, out calisan);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return calisan;
        }
        private Sirket SirketGetir(int id)
        {
            Sirket sirket;
            HttpResponseMessage result = SirketFactory.Factory.GetSirketById(id, out sirket);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return sirket;
        }
    }
}
