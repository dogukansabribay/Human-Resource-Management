using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.ViewModels
{
   public class SirketViewModel
    {
        public string SirketAdi { get; set; }
        public string Logo { get; set; }

        [NotMapping]
        public IFormFile ResimDosyasi { get; set; }
        public string YoneticiAdi { get; set; }
        public string YoneticiSoyadi { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Düzgün bir mail adresi gir.")]
        public string YoneticiEmail { get; set; }
        public DateTime YoneticiDogumTarihi { get; set; }

        public UyelikPlani UyelikPlani { get; set; }
    }
}
