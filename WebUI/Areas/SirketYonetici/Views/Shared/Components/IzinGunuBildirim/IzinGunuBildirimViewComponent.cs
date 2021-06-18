using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SirketYonetici.Views.Shared.Components.IzınGunuBildirim
{
    public class IzinGunuBildirimViewComponent : ViewComponent
    {
        private readonly EfIzinDal _efIzinDal;

        public IzinGunuBildirimViewComponent(KolayIkContext context)
        {
            _efIzinDal = new EfIzinDal(context);
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

        private Bildirim SablonGetir()
        {
            int id = KullaniciId();
            if (id == 0) return (Bildirim)Enumerable.Empty<Bildirim>();
            Calisan calisan = CalisanGetir(id);
            HttpResponseMessage result = BildirimFactory.Factory.GetBildirimler(out IEnumerable<Bildirim> sablonlar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            Bildirim sablon = sablonlar.Where(a => a.CalisanId == id && a.GonderilecekMi == true && a.SablonTanimi == SablonTanimi.IyiTatiller && a.SablonVsBildirim == SablonVsBildirim.Sablon).FirstOrDefault();//Gün kontrol
            return sablon;
        }

        public bool IzınBildirimGonderimi()
        {
            List<Izin> ızınGunu = _efIzinDal.IzınBildirimiGonder();
            Bildirim sablon = SablonGetir();//yonetici

            if (sablon != null)
            {
                if (ızınGunu != null)
                {
                    bool result = BildirimGonderildiMi(ızınGunu, sablon);
                    if (result)
                    {
                        foreach (var item in ızınGunu)
                        {
                            Bildirim bildirim = new Bildirim
                            {
                                CalisanId = item.CalisanId,
                                Baslik = sablon.Baslik,
                                Icerik = sablon.Icerik,
                                SablonTanimi = SablonTanimi.IyiTatiller,
                                SablonVsBildirim = SablonVsBildirim.Bildirim,
                                OkunduMu = false,
                                GonderilmeTarihi = DateTime.Now

                            };
                            BildirimFactory.Factory.AddBildirim(bildirim);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool BildirimGonderildiMi(IEnumerable<Izin> calisanlar, Bildirim sablon)
        {
            BildirimFactory.Factory.GetBildirimler(out IEnumerable<Bildirim> bildirimler);

            foreach (var item in bildirimler)
            {
                foreach (var items in calisanlar)
                {
                    if (item.CalisanId == items.CalisanId)
                    {
                        if (item.Baslik == sablon.Baslik && item.Icerik == sablon.Icerik && item.SablonVsBildirim == SablonVsBildirim.Bildirim && item.SablonTanimi == SablonTanimi.IyiTatiller && item.GonderilmeTarihi.Year == DateTime.Now.Year && item.GonderilmeTarihi.Month == DateTime.Now.Month && item.GonderilmeTarihi.Day == DateTime.Now.Day)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public IViewComponentResult Invoke()
        {
            bool izinGonder = IzınBildirimGonderimi();
            return View(izinGonder);
        }
    }
}
