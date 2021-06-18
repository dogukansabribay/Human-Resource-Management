using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SiteYonetici.Controllers
{
    [Area("SiteYonetici")]
    public class UyelikPlaniController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<UyelikPlani> planlar = UyelikPlanlariniGetir();
            return View(planlar);
        }
        public IActionResult UyelikPasif(int id)
        {
            SirketUyelikPlani plan;
            SirketUyelikPlaniFactory.Factory.GetPlanById(id, out plan);
            plan.IsActive = false;
            SirketUyelikPlaniFactory.Factory.Guncelle(id,plan);
            return RedirectToAction("index","home");
        }
        public IEnumerable<UyelikPlani> UyelikPlanlariniGetir() 
        {
            IEnumerable<UyelikPlani> UyelikPlanlari;
            UyelikPlaniFactory.Factory.GetUyelikPlanlari(out UyelikPlanlari);
            return UyelikPlanlari;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UyelikPlani uyelikPlani)
        {
            if (ModelState.IsValid)
            {
                uyelikPlani.CreatedDate = DateTime.Now;
                uyelikPlani.SirketUyelikPlani = null;
                UyelikPlaniFactory.Factory.UyelikPlaniEkle(uyelikPlani);
                return View("index", UyelikPlanlariniGetir());
            }
            return View(uyelikPlani);
        }

        public IActionResult Edit(int id)
        {
            UyelikPlani uyelikPlani;
            UyelikPlaniFactory.Factory.GetUyelikPlaniById(id, out uyelikPlani);
            return View(uyelikPlani);
        }

        [HttpPost]
        public IActionResult Edit(int id, UyelikPlani uyelikPlani)
        {
            if (id!=uyelikPlani.UyelikPlaniID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                UyelikPlaniFactory.Factory.Guncelle(id, uyelikPlani);

                }
                catch (Exception ex)
                {                   
                    throw new Exception(ex.Message);
                }
                return View("index",UyelikPlanlariniGetir());
            }
            return View(uyelikPlani);
        }
        public IActionResult Sil(int id)
        {
            UyelikPlani uyelikPlani;
            UyelikPlaniFactory.Factory.GetUyelikPlaniById(id, out uyelikPlani);
            return View(uyelikPlani);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            UyelikPlaniFactory.Factory.Sil(id);
            return View("index", UyelikPlanlariniGetir());
        }
    }
}
