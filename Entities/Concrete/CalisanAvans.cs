using Entities.Abstract;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
  public  class CalisanAvans: BaseEntity
    {
        public int CalisanAvansID { get; set; }
        public int? TaksitSayısı { get; set; }


        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal AvansMiktarı { get; set; }
        public string? Acıklama { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "İstenen Tarih")]
        public DateTime AvansTarihi { get; set; }

        public OnayDurumu OnayDurumu { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Talep Tarihi")]
        public DateTime AvansTalepEdilenTarih { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Onay Tarihi")]
        public DateTime AvansOnaylandıgıTarih { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Teslim Tarihi")]
        public DateTime AvansVerildigiTarih { get; set; }

        public string? RedAciklama { get; set; }

        public Calisan Calisan { get; set; }

        public int CalisanID { get; set; }
    }
}
