using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BaglantiSosyalMedya : BaseEntity
    {
        public int BaglantiSosyalMedyaID { get; set; }

        public string HesapTuru { get; set; }

        public string BaglantiAdresi { get; set; }

        public int CalisanID { get; set; }

        public Calisan Calisan { get; set; }
    }

}
