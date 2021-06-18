using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using WebTestUI.Factories;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace WebTestUI.Controllers
{
    public class BildirimController : Controller
    {
        private readonly KolayIkContext _context;

        public BildirimController(KolayIkContext context)
        {
            _context = context;
        }
        public ActionResult GetBildirimSayisi()
        {
            BildirimFactory.Factory.GetBildirimler(out IEnumerable<Bildirim> bildirimler);
            bildirimler = bildirimler.Where(a => a.CalisanId == KullaniciId() && !a.OkunduMu);
            var count = bildirimler.Count();
            var data = new { count };
            return Json(data);
        }
        // GET: Bildirim
        public IActionResult Index()
        {
           
            return View(BildirimleriGetir());
        }
        public IActionResult BildirimDetay(int id)
        {
            Bildirim bildirim = BildirimGetir(id);
            bildirim.OkunmaTarihi = DateTime.Now;
            bildirim.OkunduMu = true;
            BildirimFactory.Factory.Guncelle(id, bildirim);            
            return View(BildirimGetir(id));
        }        

        private IEnumerable<Bildirim> BildirimleriGetir()
        {
            HttpResponseMessage result = BildirimFactory.Factory.GetBildirimler(out IEnumerable<Bildirim> bildirimler);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            bildirimler = bildirimler.Where(a => a.CalisanId == KullaniciId() && !a.OkunduMu);
            return bildirimler;
        }
        private Bildirim BildirimGetir(int bildirimId)
        {
            HttpResponseMessage result = BildirimFactory.Factory.GetBildirimById(bildirimId, out Bildirim bildirim);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return bildirim;
        }
        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }     
        // GET: Bildirim/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bildirim = BildirimGetir((int)id);
            if (bildirim == null)
            {
                return NotFound();
            }

            return View(bildirim);
        }

        // GET: Bildirim/Create
        public IActionResult Create()
        {
            //ViewData["SablonId"] = new SelectList(_context.Sablonlar, "SablonId", "Baslik");
            return View();
        }

        // POST: Bildirim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BildirimId,SablonId,OkunduMu,OkunmaTarihi,CreatedDate,ModifiedDate")] Bildirim bildirim)
        {
            if (ModelState.IsValid)
            {
                BildirimFactory.Factory.AddBildirim(bildirim);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["SablonId"] = new SelectList(_context.Sablonlar, "SablonId", "Baslik", bildirim.SablonId);
            return View(bildirim);
        }

        // GET: Bildirim/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BildirimFactory.Factory.GetBildirimById((int)id, out Bildirim bildirim);
            if (bildirim == null)
            {
                return NotFound();
            }
            //ViewData["SablonId"] = new SelectList(_context.Sablonlar, "SablonId", "Baslik", bildirim.SablonId);
            return View(bildirim);
        }

        // POST: Bildirim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BildirimId,SablonId,OkunduMu,OkunmaTarihi,CreatedDate,ModifiedDate")] Bildirim bildirim)
        {
            if (id != bildirim.BildirimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BildirimFactory.Factory.Guncelle(id,bildirim);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BildirimExists(bildirim.BildirimId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["SablonId"] = new SelectList(_context.Sablonlar, "SablonId", "Baslik", bildirim.SablonId);
            return View(bildirim);
        }

        // GET: Bildirim/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BildirimFactory.Factory.GetBildirimById((int)id, out Bildirim bildirim);
            if (bildirim == null)
            {
                return NotFound();
            }

            return View(bildirim);
        }

        // POST: Bildirim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            BildirimFactory.Factory.GetBildirimById(id, out Bildirim bildirim);
            BildirimFactory.Factory.Sil(bildirim);
            return RedirectToAction(nameof(Index));
        }

        private bool BildirimExists(int id)
        {
            BildirimFactory.Factory.GetBildirimById(id, out Bildirim bildirim);
            if (bildirim!=null)
            {
                return true;
            }
            return false;
        }
    }
}
