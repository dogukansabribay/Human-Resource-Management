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
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using WebTestUI.Factories;

namespace WebTestUI.Controllers
{
    public class CalisanAvansController : Controller
    {
        private readonly KolayIkContext _context;

        //private readonly UnitOfWork _work;
        //private readonly IWebHostEnvironment Environment;
        private readonly EfCalisanAvansDal _efCalisanAvansDal;
        
        public CalisanAvansController(KolayIkContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            //_work = new UnitOfWork(_context);
            //Environment = _environment;
            _efCalisanAvansDal = new EfCalisanAvansDal(context);
        }
        private CalisanAvans CalisanAvansGetir(int id)
        {
            HttpResponseMessage result = CalisanAvansFactory.Factory.GetAvansById(id, out CalisanAvans avans);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return avans;
        }
        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }
        private IEnumerable<CalisanAvans> CalisanAvanslariGetir()
        {
            HttpResponseMessage result = CalisanAvansFactory.Factory.GetAvanslar(out IEnumerable<CalisanAvans> Avanslar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return Avanslar.Where(a=>a.CalisanID==KullaniciId());
        }
        // GET: CalisanAvans
        public IActionResult Index()
        {            
            return View(CalisanAvanslariGetir());
        }

        // GET: CalisanAvans/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanAvans = await _context.CalisanAvanslar
                .FirstOrDefaultAsync(m => m.AvansTarihi == id);
            if (calisanAvans == null)
            {
                return NotFound();
            }

            return View(calisanAvans);
        }

        // GET: CalisanAvans/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: CalisanAvans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CalisanAvansID,TaksitSayısı,AvansMiktarı,Acıklama,AvansTarihi,AvansTalepEdilenTarih,OnaylandiMi,AvansOnaylandıgıTarih,AvansVerildigiTarih")] CalisanAvans calisanAvans)
        {
            calisanAvans.CalisanID = KullaniciId();
            if (ModelState.IsValid)
            {
               


                if (!_efCalisanAvansDal.AvansTalepTarihKontrol(calisanAvans))
                {
                    ViewBag.GecmisTarihliAvansTalep = "Geçmiş tarihli avans talebi oluşturamazsınız.";
                    return View(calisanAvans);
                }
                _efCalisanAvansDal.DefaultAyarlar(calisanAvans);
                CalisanAvansFactory.Factory.AddAvans(calisanAvans);
               
                return RedirectToAction("Index", "Home");
            }
            return View(calisanAvans);

        }

        // GET: CalisanAvans/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanAvans = CalisanAvansGetir((int)id);
            if (calisanAvans == null)
            {
                return NotFound();
            }
            return View(calisanAvans);
        }

        // POST: CalisanAvans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CalisanAvansID,TaksitSayısı,AvansMiktarı,Acıklama,AvansTarihi")] CalisanAvans calisanAvans)
        {
            if (id != calisanAvans.CalisanAvansID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(calisanAvans);
                    //await _context.SaveChangesAsync();
                    CalisanAvansFactory.Factory.Guncelle(id, calisanAvans);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanAvansExists(calisanAvans.CalisanAvansID))
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
            return View(calisanAvans);
        }

        // GET: CalisanAvans/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisanAvans = CalisanAvansGetir((int)id);
            if (calisanAvans == null)
            {
                return NotFound();
            }

            return View(calisanAvans);
        }

        // POST: CalisanAvans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            CalisanAvansFactory.Factory.Sil(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanAvansExists(int id)
        {
            return _context.CalisanAvanslar.Any(e => e.CalisanAvansID == id);
        }
    }
}
