using DataAccess.Concrete.EntityFramework;
using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTestUI.Areas.SirketYonetici.Views.Shared.Components.CalisanIzin
{
    public class CalisanIzinViewComponent:ViewComponent
    {

        private readonly EfIzinDal efIzinDal;
        private readonly EfCalisanDal _efCalisanDal;
        public CalisanIzinViewComponent(KolayIkContext context)
        {
            _efCalisanDal = new EfCalisanDal(context);
            efIzinDal = new EfIzinDal(context);
        }

        public IViewComponentResult Invoke() 
        {
            int id = KullaniciId();
            Calisan calisan = _efCalisanDal.GetById(id);
            int sirketId = (int)calisan.SirketId;
            var calisanList = _efCalisanDal.GetAll(a => a.SirketId == sirketId).ToList();

            var toplamKullanılan = calisanList.Sum(a => a.KullandigiYıllıkIzinSayisi);
            var toplamKalan = calisanList.Sum(a => a.KalanYıllıkIzinSayisi);
        

            CalisanIzinViewModel izin = new CalisanIzinViewModel();
            izin.KullanılanYillikIzinSayisi = toplamKullanılan;
            izin.KalanYillikIzinSayisi = toplamKalan;
            

            return View(izin);
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
