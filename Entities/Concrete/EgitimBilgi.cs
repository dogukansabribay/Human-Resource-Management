using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class EgitimBilgi:BaseEntity
    {
        public int EgitimBilgiId { get; set; }
        public string OkulAdi { get; set; }
        public EgitimSeviyesi EgitimSeviyesi { get; set; }
        public DateTime MezuniyetTarihi { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }


    }
}
