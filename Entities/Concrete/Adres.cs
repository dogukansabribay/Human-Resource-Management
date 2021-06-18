using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Adres : BaseEntity
    {
        public int AdresId { get; set; }
        public int CalisanID { get; set; }
        public Calisan Calisan { get; set; }
        public string AdresAdi { get; set; }
        public string AdresDevam { get; set; }
        public string EvTelefonu { get; set; }
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string PostaKodu { get; set; }
        // Doğukan için
    }
}
