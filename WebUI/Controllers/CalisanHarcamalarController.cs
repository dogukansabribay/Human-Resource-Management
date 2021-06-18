using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete;
using System.IO;
using Entities.Concrete.Enums;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using WebTestUI.Factories;

namespace WebTestUI.Controllers
{
    public class CalisanHarcamalarController : Controller
    {
        private readonly KolayIkContext _context;
        //private readonly UnitOfWork _work;
        private EfCalisanHarcamaDal EfCalisanHarcamaDal;
        private readonly IWebHostEnvironment Environment;

        public CalisanHarcamalarController(KolayIkContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            //_work = new UnitOfWork(_context);
            Environment = _environment;
            EfCalisanHarcamaDal = new EfCalisanHarcamaDal(context);
        }
        private CalisanHarcama CalisanHarcamaGetir(int id)
        {
            CalisanHarcama harcama;
            HttpResponseMessage result = CalisanHarcamaFactory.Factory.GetHarcamaById(id, out harcama);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return harcama;
        }
        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }
        private IEnumerable<CalisanHarcama> CalisanHarcamalariGetir()
        {
            IEnumerable<CalisanHarcama> Harcamalar;
            HttpResponseMessage result = CalisanHarcamaFactory.Factory.GetHarcamalar(out Harcamalar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return Harcamalar.Where(a => a.CalisanId == KullaniciId());
        }
        public IActionResult HarcamaDetay(int id)
        {
            //CalisanHarcama harcama = EfCalisanHarcamaDal.GetById(id);
            CalisanHarcama harcama = CalisanHarcamaGetir(id);
            if (harcama == null)
            {
                return NotFound();
            }

            return View(harcama);
        }

        public IActionResult GecmisHarcamalar()
        {
            //List<CalisanHarcama> list =_context.CalisanHarcamalari.ToList();
            //return View(list);
            return View();
        }

        public IActionResult DetaylarSayfasi()
        {
            return View();
        }

        // GET: CalisanHarcamalar
        public IActionResult Index()
        {
            return View(CalisanHarcamalariGetir());
        }

        // GET: CalisanHarcamalar/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanHarcama = CalisanHarcamaGetir((int)id);
            if (calisanHarcama == null)
            {
                return NotFound();
            }

            return View(calisanHarcama);
        }

        // GET: CalisanHarcamalar/Create
        public IActionResult Create()
        {
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi");
            return View();
        }

        // POST: CalisanHarcamalar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CalisanHarcamaID,HarcamaAciklamasi,HarcamaMiktari,HarcamaBelgesi,HarcamaTarihi,HarcamaBelgesiYolu,CalisanId")] CalisanHarcama calisanHarcama)
        {
            calisanHarcama.CalisanId = KullaniciId();
            if (ModelState.IsValid)
            {

                //_work.EfCalisanHarcamaDal.Add(calisanHarcama);
                //EfCalisanHarcamaDal.Add(calisanHarcama);



                string resimler = Path.Combine(this.Environment.WebRootPath, "HarcamaResimler");
                if (calisanHarcama.HarcamaBelgesi != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(resimler, calisanHarcama.HarcamaBelgesi.FileName), FileMode.Create))
                    {
                        calisanHarcama.HarcamaBelgesi.CopyToAsync(fileStream);
                    }
                    calisanHarcama.HarcamaBelgesiYolu = calisanHarcama.HarcamaBelgesi.FileName;
                }
                calisanHarcama.HarcamaBelgesi = null;
                CalisanHarcamaFactory.Factory.AddHarcama(calisanHarcama);
                //EfCalisanHarcamaDal.Update(calisanHarcama);
                //değişti
                //CalisanHarcamaFactory.Factory.Guncelle(calisanHarcama.CalisanHarcamaID, calisanHarcama);
                //_work.Complete();
                return RedirectToAction("index", "home");
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", calisanHarcama.CalisanId);
            return View(calisanHarcama);
        }

        // GET: CalisanHarcamalar/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanHarcama = CalisanHarcamaGetir((int)id);
            if (calisanHarcama == null)
            {
                return NotFound();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", calisanHarcama.CalisanId);
            return View(calisanHarcama);
        }

        // POST: CalisanHarcamalar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CalisanHarcamaID,HarcamaAciklamasi,HarcamaMiktari,HarcamaBelgesiYolu,CalisanId")] CalisanHarcama calisanHarcama)
        {
            if (id != calisanHarcama.CalisanHarcamaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CalisanHarcamaFactory.Factory.Guncelle(id, calisanHarcama);
                    //_context.Update(calisanHarcama);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanHarcamaExists(calisanHarcama.CalisanHarcamaID))
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
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "Adi", calisanHarcama.CalisanId);
            return View(calisanHarcama);
        }

        // GET: CalisanHarcamalar/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanHarcama = CalisanHarcamaGetir((int)id);
            if (calisanHarcama == null)
            {
                return NotFound();
            }

            return View(calisanHarcama);
        }

        // POST: CalisanHarcamalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            CalisanHarcamaFactory.Factory.Sil(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanHarcamaExists(int id)
        {
            return _context.CalisanHarcamalari.Any(e => e.CalisanHarcamaID == id);
        }


    }
}
