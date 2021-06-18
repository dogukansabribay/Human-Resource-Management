using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SiteYonetici.Views.Shared.Components.PaketBitimiKontrol
{
    public class PaketBitimiKontrolViewComponent : ViewComponent
    {
        private readonly EfSirketUyelikPlaniDal _efSirketUyelik;
        private readonly EfCalisanDal _efCalisanDal;
        public PaketBitimiKontrolViewComponent(KolayIkContext context)
        {
            _efSirketUyelik = new EfSirketUyelikPlaniDal(context);
            _efCalisanDal = new EfCalisanDal(context);
        }

        public IViewComponentResult Invoke()
        {
            bool result = Kontrol();
            return View(result);

        }

        public bool Kontrol()
        {
            List<SirketUyelikPlani> sirketler = _efSirketUyelik.GetAll(a => a.BitisTarihi == DateTime.Now.AddMonths(2));
            if (sirketler != null)
            {
                List<Calisan> yoneticiler = new List<Calisan>();
                foreach (var item in sirketler)
                {
                    Calisan calisan = _efCalisanDal.Get(a => a.SirketId == item.SirketID && a.ErisimTuru == Entities.Concrete.Enums.ErisimTuru.Yonetici);
                    yoneticiler.Add(calisan);
                }

                foreach (var item in yoneticiler)
                {
                    Bildirim bildirim = new Bildirim();
                    bildirim.CalisanId = item.CalisanId;
                    bildirim.Baslik = "Üyelik Planınızı Yenileyin.";
                    bildirim.Icerik = "Üyelik planınız 2 ay sonra bitecek. Lütfen platformumuzu kullanmaya devam etmek için üyelik planınızı yenileyin";
                    bildirim.OkunduMu = false;
                    bildirim.SablonVsBildirim = SablonVsBildirim.Bildirim;
                    BildirimFactory.Factory.AddBildirim(bildirim);
                }
                return true;
            }
            return false;
        }
    }
}
