using DataAccess.Concrete.EntityFramework;
using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTestUI.Areas.SirketYonetici.Controllers
{
    [Area("SirketYonetici")]
    public class HomeController : Controller
    {
        private readonly KolayIkContext _context;
     
        public HomeController(KolayIkContext context)
        {
            _context = context;
            
        }

        // localhost:PortNo/Admin/Home/Index
        [Route("/SirketYonetici/Home/index")]
        public IActionResult Index()
        {
            return View();
        }

    

    }
}
