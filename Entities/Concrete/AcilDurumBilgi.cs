using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AcilDurumBilgi : BaseEntity
    {
        public int AcilDurumBilgiId { get; set; }

        public string AranacakKisiAd { get; set; }

        public string AranacakKisiSoyad { get; set; }

        public string TelefonNo { get; set; }

        public string YakinlikDerecesi { get; set; }

        public int CalisanId { get; set; }

        public Calisan Calisan { get; set; }
    }
}
