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

namespace WebTestUI.Areas.SirketYonetici.Views.Shared.Components.DogumGunuKutlama
{
    public class DogumGunuKutlamaViewComponent : ViewComponent
    {
        private readonly EfCalisanDal _efCalisanDal;

        public DogumGunuKutlamaViewComponent(KolayIkContext context)
        {
            _efCalisanDal = new EfCalisanDal(context);
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
            Bildirim sablon = sablonlar.Where(a => a.CalisanId == id && a.GonderilecekMi == true && a.SablonTanimi == SablonTanimi.DogumGunu && a.SablonVsBildirim == SablonVsBildirim.Sablon).FirstOrDefault();//Gün kontrol
            return sablon;
        }

        public bool DogumGunuKutlamaGonder()
        {

            List<Calisan> dogumGunu = _efCalisanDal.DogumGunuKutlanacakCalisanlar();

            if (dogumGunu != null)
            {
                Bildirim sablon = SablonGetir();//yonetici
                if (sablon != null)
                {
                    bool result = BildirimGonderildiMi(dogumGunu, sablon);
                    if (result)
                    {

                        foreach (var item in dogumGunu)
                        {
                            Bildirim bildirim = new Bildirim
                            {
                                CalisanId = item.CalisanId,
                                Baslik = sablon.Baslik,
                                Icerik = sablon.Icerik,
                                SablonTanimi = SablonTanimi.DogumGunu,
                                SablonVsBildirim = SablonVsBildirim.Bildirim,//yöneticinin oluşturduğu şablon, Kullanıcıya bildirim olarak gidecek
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

        public bool BildirimGonderildiMi(IEnumerable<Calisan> calisanlar, Bildirim sablon)
        {
            BildirimFactory.Factory.GetBildirimler(out IEnumerable<Bildirim> bildirimler);

            foreach (var item in bildirimler)
            {
                foreach (var items in calisanlar)
                {
                    if (item.CalisanId == items.CalisanId)
                    {
                        if (item.Baslik == sablon.Baslik && item.Icerik == sablon.Icerik && item.SablonVsBildirim == SablonVsBildirim.Bildirim && item.SablonTanimi == SablonTanimi.DogumGunu && item.GonderilmeTarihi.Year == DateTime.Now.Year && item.GonderilmeTarihi.Month == DateTime.Now.Month && item.GonderilmeTarihi.Day == DateTime.Now.Day)
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
            bool dogumGunuKutlama = DogumGunuKutlamaGonder();
            return View(dogumGunuKutlama);
        }

    }
}
