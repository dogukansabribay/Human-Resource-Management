using Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class KullaniciYorumu : BaseEntity
    {
        public int KullaniciYorumuId { get; set; }
        public string YorumDetay { get; set; }
        public YorumTanimi YorumTanimi { get; set; }
        public bool YayinlansinMi { get; set; }
        public bool OkunduMu { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
    }
}
