using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class ResmiTatilViewModel
    {
        public string Aciklama { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Baslangıc Tarihi")]
        public DateTime TatilTarihi { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Bitis Tarihi")]
        public DateTime TatilBitisTarihi { get; set; }


    }
}
