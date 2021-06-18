using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Abstract;
using Entities.Concrete.Enums;

namespace Entities.Concrete
{
    public class CalisanKariyer : BaseEntity
    {
        public int CalisanKariyerId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="İşe Giriş Tarihi")]
        public DateTime IseGirisTarihi { get; set; }


        [DataType(DataType.Date)]
        [Display(Name ="Pozisyon Değişim Tarihi")]
        public DateTime IstenCikisTarihi { get; set; }

        [Display(Name ="Çalışma Şekli")]
        public string CalismaSekli { get; set; }

       
        [Display(Name ="Şube")]
        public string Sube { get; set; }

        [Display(Name = "Departman")]
        public DepartmanTipi Departman { get; set; }

        [Display(Name = "Unvan")]
        public UnvanTipi Unvan { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Görev Tanımı")]
        public string GorevTanimi { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Yetenekler")]
        public string YetenekTanimi { get; set; }

        public int Maas { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }



    }
}
