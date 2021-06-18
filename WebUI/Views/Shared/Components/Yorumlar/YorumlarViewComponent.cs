using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTestUI.Views.Shared.Components.Yorumlar
{
    public class YorumlarViewComponent : ViewComponent
    {
        private readonly EfKullaniciYorumuDal _yorumDal;
        private readonly EfTarihSecimiDal _efTarihSecimiDal;
        private readonly EfCalisanDal _efCalisan;
        public YorumlarViewComponent(KolayIkContext context)
        {
            _yorumDal = new EfKullaniciYorumuDal(context);
            _efTarihSecimiDal = new EfTarihSecimiDal(context);
            _efCalisan = new EfCalisanDal(context);
        }

        public IViewComponentResult Invoke()
        {
            List<TarihSecimi> tarihSecimleri = _efTarihSecimiDal.GetAll();
            TarihSecimi tarihsecimi = tarihSecimleri.OrderBy(a => a.CreatedDate).Take(1).FirstOrDefault();
            List<KullaniciYorumu> yorums = new List<KullaniciYorumu>();
            if (tarihsecimi != null)
            {
                yorums = _yorumDal.GetAll(a => a.YayinlansinMi && a.CreatedDate>= tarihsecimi.BaslangicTarihi && a.CreatedDate<= tarihsecimi.BitisTarihi);//calisan ad soyad al
                if (yorums!=null)
                {
                    foreach (var item in yorums)
                    {
                        if (item.YorumDetay.Length>25)
                        {
                            item.YorumDetay = item.YorumDetay.Substring(0, 20);
                        }
                        item.YorumDetay = item.YorumDetay;
                        Calisan calisan = _efCalisan.GetById(item.CalisanId);
                        item.Calisan.Adi = calisan.Adi+" "+calisan.Soyadi;
                    }
                } 
            }
            return View(yorums);
        }
    }
}
