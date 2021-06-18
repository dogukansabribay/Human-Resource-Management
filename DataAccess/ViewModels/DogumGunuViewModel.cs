using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.ViewModels
{
  public  class DogumGunuViewModel
    {
        public string CalisanAd { get; set; }

        public string CalisanSoyad { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CalisanDogumGunu { get; set; }
    }
}
