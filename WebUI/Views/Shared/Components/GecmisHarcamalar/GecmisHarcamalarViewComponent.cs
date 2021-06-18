using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Views.Shared.Components.GecmisHarcamalar
{
    public class GecmisHarcamalarViewComponent:ViewComponent
    {
        private readonly KolayIkContext _context;
        private EfCalisanHarcamaDal EfCalisanHarcamaDal;
        public GecmisHarcamalarViewComponent(KolayIkContext context)
        {
            _context = context;
            EfCalisanHarcamaDal = new EfCalisanHarcamaDal(context);
        }

        public IViewComponentResult Invoke()
        {
            int id;
            if (Request.Cookies["UserId"] == null)
            {
                id= 0;
            }
            id= int.Parse(Request.Cookies["UserId"]);
            IEnumerable<CalisanHarcama> Harcamalar;
            HttpResponseMessage result = CalisanHarcamaFactory.Factory.GetHarcamalar(out Harcamalar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);            
            return View(Harcamalar.Where(a => a.CalisanId == id));
        }
    }
}
