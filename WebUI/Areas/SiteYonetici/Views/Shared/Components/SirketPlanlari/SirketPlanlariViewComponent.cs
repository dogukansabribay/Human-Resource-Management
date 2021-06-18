using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SiteYonetici.Views.Components.SirketPlanlari
{
    public class SirketPlanlariViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<SirketUyelikPlanlariListeViewModel> planlarVM = new List<SirketUyelikPlanlariListeViewModel>();
            IEnumerable<SirketUyelikPlani> planlar;
            SirketUyelikPlaniFactory.Factory.GetPlanlar(out planlar);
            foreach (var item in planlar)
            {
                if (item.IsActive && item.BitisTarihi<DateTime.Now.AddMonths(2))
                {
                    Sirket sirket = new Sirket();
                    UyelikPlani plan = new UyelikPlani();
                    SirketFactory.Factory.GetSirketById(item.SirketID, out sirket);
                    UyelikPlaniFactory.Factory.GetUyelikPlaniById(item.UyelikPlaniID, out plan);
                    planlarVM.Add(new SirketUyelikPlanlariListeViewModel { Id = (int)item.SirketUyelikPlaniID, BaslangicTarihi = (DateTime)item.BaslangicTarihi, BitisTarihi = (DateTime)item.BitisTarihi, IsActive = item.IsActive, PlanUcreti = plan.PlanUcreti, SirketAdi = sirket.Adi, UyelikTanimi = plan.UyelikPlaniTanimi });
                }

            }
            return View(planlarVM);
        }
    }
}
