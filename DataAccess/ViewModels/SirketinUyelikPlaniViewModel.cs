using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ViewModels
{
    public class SirketinUyelikPlaniViewModel
    {
        public string UyelikPlaniTanimi { get; set; }
        public int MaksCalisanSayisi { get; set; }
        public int AktifCalisanSayisi { get; set; }
        public double PlanUcreti { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public int CalisanYuzdesi { get; set; }

        public int ZamanYuzdesi { get; set; }
    }
}
