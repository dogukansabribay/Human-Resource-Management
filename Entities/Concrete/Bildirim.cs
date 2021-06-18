using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Bildirim : BaseEntity
    {
        public int BildirimId { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
        public bool OkunduMu { get; set; }
        public DateTime? OkunmaTarihi { get; set; }

        [Display(Name = "Tanım")]
        public SablonTanimi? SablonTanimi { get; set; }
        public bool GonderilecekMi { get; set; }

        public DateTime GonderilmeTarihi { get; set; }

        public SablonVsBildirim SablonVsBildirim { get; set; }
    }
    public enum SablonVsBildirim
    {
        Sablon,
        Bildirim
    }

}
