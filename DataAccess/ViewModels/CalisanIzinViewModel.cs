using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ViewModels
{
   public class CalisanIzinViewModel
    {
        public string FullName { get; set; }

        public  IzinTanimi IzinTanimi { get; set; } 
        public int? ToplamIzinSayisi { get; set; }
        public int? KullanılanYillikIzinSayisi { get; set; }
        public int? OnayBekleyenIzinSayisi { get; set; }
        public int? KalanYillikIzinSayisi { get; set; }


    }
}
