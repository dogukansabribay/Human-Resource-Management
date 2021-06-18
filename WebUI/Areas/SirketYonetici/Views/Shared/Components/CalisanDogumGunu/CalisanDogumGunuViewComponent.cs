using DataAccess.Concrete.EntityFramework;
using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SirketYonetici.Views.Shared.Component.CalisanDogumGunleri
{
    public class CalisanDogumGunuViewComponent : ViewComponent
    {
        private readonly KolayIkContext _context;

        public CalisanDogumGunuViewComponent(KolayIkContext context)
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
        //web api--dogum gunu gune göre sıralama geliştir
        public IViewComponentResult Invoke()
        {
            Calisan calisan;
            HttpResponseMessage response = CalisanFactory.Factory.GetCalisanById(KullaniciId(), out calisan);
            int sirketId=(int)calisan.SirketId;
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanlar(out IEnumerable<Calisan> DogumGunleri);

            DogumGunleri = DogumGunleri
                .OrderBy(b => b.DogumTarihi.Value.Day)
                .Where(a => a.DogumTarihi.Value.Month == DateTime.Now.Month && a.DogumTarihi.Value.Day >= DateTime.Now.Day && a.SirketId == sirketId)
                .ToList();
            
            
            List<DogumGunuViewModel> dogumGunuList = DogumGunleri.Select(e => new DogumGunuViewModel { CalisanAd = e.Adi, CalisanSoyad = e.Soyadi, CalisanDogumGunu = e.DogumTarihi }).ToList();

            if (dogumGunuList!=null)
            {
                return View(dogumGunuList);
            }
            return View();
        }


    }
}
