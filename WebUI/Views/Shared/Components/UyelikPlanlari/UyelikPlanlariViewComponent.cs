using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Views.Shared.Components.UyelikPlanlari
{
    public class UyelikPlanlariViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            UyelikPlaniFactory.Factory.GetUyelikPlanlari(out IEnumerable<UyelikPlani> uyelikPlani);
            return View(uyelikPlani);
        }
    }
}
