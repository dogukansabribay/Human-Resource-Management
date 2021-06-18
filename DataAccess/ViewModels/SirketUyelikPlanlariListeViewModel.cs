using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.ViewModels
{
   public  class SirketUyelikPlanlariListeViewModel
    {
        public int Id { get; set; }
        public string SirketAdi { get; set; }
        public string UyelikTanimi { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }
        [DataType(DataType.Date)]
        public DateTime BitisTarihi { get; set; }
        [DataType(DataType.Currency)]
        public double PlanUcreti { get; set; }
    }
}
