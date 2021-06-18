using DataAccess.Concrete.EntityFramework;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;


namespace WebTestUI.Views.Shared.Components.ResmiTatil
{
    public class ResmiTatilViewComponent:ViewComponent
    {
                private readonly KolayIkContext _context;

        public ResmiTatilViewComponent(KolayIkContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            List<ResmiTatilViewModel> tatilListesi = _context.ResmiTatiller.Select(e => new ResmiTatilViewModel { Aciklama = e.Aciklama, TatilTarihi = e.BaslangicTarihi, TatilBitisTarihi=e.BitisTarihi}).Where(a=>a.TatilTarihi > DateTime.Now).OrderBy(a=>a.TatilTarihi).Take(6).ToList();
            return View(tatilListesi);
        }


    }
}
