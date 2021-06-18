using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entities.Concrete;
using System.Net.Http;
using WebTestUI.Factories;
using DataAccess.ViewModels;

namespace WebTestUI.Views.Shared.Components.CalisanAvanslar
{
    public class CalisanAvanslarViewComponent:ViewComponent
    {
        private readonly KolayIkContext _context;

        public CalisanAvanslarViewComponent(KolayIkContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<CalisanAvans> Avanslar;
            HttpResponseMessage result = CalisanAvansFactory.Factory.GetAvanslar(out Avanslar);
            Avanslar = Avanslar.Where(a => a.CalisanID == int.Parse(Request.Cookies["UserId"]));

            //List<CalisanAvansViewModel> avansList = _context.CalisanAvanslar.Select(e => new CalisanAvansViewModel { AvansMiktarı=e.AvansMiktarı,AvansTarihi=e.AvansTarihi,AvansOnaylandıgıTarih=e.AvansOnaylandıgıTarih,AvansTalepEdilenTarih=DateTime.Now,AvansVerildigiTarih=e.AvansVerildigiTarih }).ToList();
            List<CalisanAvansViewModel> avansList = Avanslar.Where(a =>a.CalisanID== int.Parse(Request.Cookies["UserId"]) && a.OnayDurumu==Entities.Concrete.Enums.OnayDurumu.Onaylı).Select(e => new CalisanAvansViewModel { AvansMiktarı=e.AvansMiktarı,AvansTarihi=e.AvansTarihi,AvansOnaylandıgıTarih=e.AvansOnaylandıgıTarih,AvansTalepEdilenTarih=DateTime.Now,AvansVerildigiTarih=e.AvansVerildigiTarih }).ToList();
            return View(avansList);
        }
    }
}
