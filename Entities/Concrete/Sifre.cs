using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Sifre : BaseEntity
    {
        public int SifreId { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
        public string Parola { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }

    }
}
