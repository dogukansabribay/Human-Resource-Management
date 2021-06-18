using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    public class ResmiTatilController : Controller
    {
        private readonly EfResmiTatilDal _efResmiTatil;

        // GET: ResmiTatilController
        public ActionResult Index()
        {
            return View();
        }

        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }


        // GET: ResmiTatilController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResmiTatilController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResmiTatilController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResmiTatilController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResmiTatilController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ResmiTatilController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResmiTatilController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
