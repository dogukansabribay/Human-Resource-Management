using DataAccess.Concrete.EntityFramework;
using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTestUI.Areas.SirketYonetici.Views.Shared.Components.SirketinUyelikPlani
{
    public class SirketinUyelikPlaniViewComponent : ViewComponent
    {
        public readonly EfSirketUyelikPlaniDal _efSirketUyelikPlaniDal;
        private readonly EfCalisanDal _efCalisanDal;
        private readonly EfUyelikPlaniDal _efUyelikPlaniDal;
        public SirketinUyelikPlaniViewComponent(KolayIkContext context)
        {
            _efSirketUyelikPlaniDal = new EfSirketUyelikPlaniDal(context);
            _efCalisanDal = new EfCalisanDal(context);
            _efUyelikPlaniDal = new EfUyelikPlaniDal(context);
        }

        public IViewComponentResult Invoke()
        {
            int id = KullaniciId();
            Calisan calisan = _efCalisanDal.GetById(id);
            int sirketId = (int)calisan.SirketId;
            SirketUyelikPlani sirketUyelikPlani = _efSirketUyelikPlaniDal.GetAll(a => a.SirketID == sirketId).FirstOrDefault();
            UyelikPlani uyelikPlani = _efUyelikPlaniDal.GetAll(a => a.UyelikPlaniID == sirketUyelikPlani.UyelikPlaniID).FirstOrDefault();
            IEnumerable<Calisan> calisans = _efCalisanDal.GetAll(a => a.SirketId == sirketId && a.AktifMi).ToList();



            SirketinUyelikPlaniViewModel model = new SirketinUyelikPlaniViewModel();

            model.BaslangicTarihi = (DateTime)sirketUyelikPlani.BaslangicTarihi;
            model.BitisTarihi = (DateTime)sirketUyelikPlani.BitisTarihi;
            model.PlanUcreti = uyelikPlani.PlanUcreti;
            model.UyelikPlaniTanimi = uyelikPlani.UyelikPlaniTanimi;
            model.MaksCalisanSayisi = uyelikPlani.CalisanSayisi;
            model.AktifCalisanSayisi = calisans.Count();
            model.ZamanYuzdesi = 100 - ((sirketUyelikPlani.BitisTarihi - DateTime.Now).Value.Days * 100 / 365);
            model.CalisanYuzdesi = (calisans.Count() * 100) / uyelikPlani.CalisanSayisi;
            ViewBag.CalisanYuzdesi = model.CalisanYuzdesi + "px";
            ViewBag.ZamanYuzdesi = model.ZamanYuzdesi + "px";
            ViewBag.KalanCalisanSayisi = model.MaksCalisanSayisi - model.AktifCalisanSayisi;
            ViewBag.KalanZaman = (sirketUyelikPlani.BitisTarihi - DateTime.Now).Value.Days / 30;
            if (model == null)
            {
                return View();
            }
            return View(model);
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
