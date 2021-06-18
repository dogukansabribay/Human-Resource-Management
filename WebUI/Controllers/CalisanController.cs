using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ViewModels;
using System.Net.Http;
using WebTestUI.Factories;

namespace WebTestUI.Controllers
{
    public class CalisanController : Controller
    {
        private readonly KolayIkContext _context;
        //private readonly UnitOfWork work;
        EfCalisanDal efCalisan;
        EfEgitimBilgiDal efEgitim;
        EfCalisanHarcamaDal efCalisanHarcama;
        EfCalisanKariyerDal efCalisanKariyerDal;
        public CalisanController(KolayIkContext context)
        {
            _context = context;
            //work = new UnitOfWork(_context);
            efCalisan = new EfCalisanDal(context);
            efEgitim = new EfEgitimBilgiDal(context);
            efCalisanHarcama = new EfCalisanHarcamaDal(context);
            efCalisanKariyerDal = new EfCalisanKariyerDal(context);
        }
        
        private Calisan CalisanGetir(int id)
        {
            Calisan calisan;
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanById(id, out calisan);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return calisan;
        }


        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }
        private IEnumerable<Calisan> CalisanlariGetir()
        {
            IEnumerable<Calisan> Calisanlar;
            HttpResponseMessage result = CalisanFactory.Factory.GetCalisanlar(out Calisanlar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return Calisanlar;
        }
        public IActionResult Index()
        {            
            return View(CalisanlariGetir());
        }

        public IActionResult CalisanGoruntuleme(string ara)
        {
            int id = KullaniciId();
            if (id== 0) return NotFound();
            Calisan calisan = CalisanGetir(id);
            IEnumerable<Calisan> calisanlar = CalisanlariGetir();
            calisanlar = calisanlar.Where(a => a.SirketId == calisan.SirketId);
           
            if (!string.IsNullOrEmpty(ara))
            {
                calisanlar = calisanlar.Where(x => x.Adi.Contains(ara) || x.Soyadi.Contains(ara)).ToList();
            }
            return View(calisanlar);
        }

        public IActionResult CalisanEgitimGoruntuleme(int id) 
        {
            int Id = KullaniciId();
            if (Id == 0) return NotFound();
            CalisanEgitimViewModel model = new CalisanEgitimViewModel();
            model.Calisan = CalisanGetir(id);
            //model.Calisan = CalisanGetir(Id);
            model.CalisanId = model.Calisan.CalisanId;
            //model.EgitimBilgileri = efEgitim.GetAll(a => a.CalisanId == id);
            model.EgitimBilgileri = CalisanEgitimleriGetir().Where(a => a.CalisanId == id).ToList();
            //model.EgitimBilgileri = efEgitim.GetAll(a => a.CalisanId == Id);
            return View(model);
        }
        private IEnumerable<EgitimBilgi> CalisanEgitimleriGetir()
        {
            IEnumerable<EgitimBilgi> EgitimBilgileri;
            HttpResponseMessage result = CalisanEgitimFactory.Factory.GetEgitimBilgileri(out EgitimBilgileri);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return EgitimBilgileri;
        }
        public IActionResult CalisanKariyerGoruntuleme(int id)
        {
            int Id = KullaniciId();
            if (Id == 0) return NotFound();

            var calisan = CalisanGetir(id);
            var kariyer = efCalisanKariyerDal.GetAll(a => a.CalisanId == calisan.CalisanId);
           
            return View(kariyer);
        }
        public IActionResult Create()
        {
            return View(new Calisan());
        }

        

        [HttpPost]
        public IActionResult Create(Calisan calisan)
        {
            //work.EfCalisanDal.Add(calisan);
            //work.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Harcamalar()        
        {
            int id = KullaniciId();
            if (id == 0) return NotFound();
            IEnumerable<CalisanHarcama> harcamalar;
            HttpResponseMessage result = CalisanHarcamaFactory.Factory.GetHarcamalar(out harcamalar);
            if (!result.IsSuccessStatusCode) ModelState.AddModelError(result.StatusCode.ToString(), result.ReasonPhrase);
            return View(harcamalar.Where(a=>a.CalisanId==id));
        }

    }
}
