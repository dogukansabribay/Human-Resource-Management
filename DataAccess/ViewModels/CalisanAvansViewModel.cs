using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class CalisanAvansViewModel
    {

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal AvansMiktarı { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "İstenen Tarih")]
        public DateTime AvansTarihi { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Talep Tarihi")]
        public DateTime AvansTalepEdilenTarih { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Onay Tarihi")]
        public DateTime AvansOnaylandıgıTarih { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Teslim Tarihi")]
        public DateTime AvansVerildigiTarih { get; set; }



    }
}
