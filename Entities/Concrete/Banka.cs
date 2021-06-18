using Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Banka : BaseEntity
    {
        public int BankaId { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
        public string BankaAdi { get; set; }
        public HesapTipi HesapTipi { get; set; }
        public string HesapNo { get; set; }
        public string IBAN { get; set; }

    }
}
