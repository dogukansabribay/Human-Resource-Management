using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.Enums
{

    public enum SablonTanimi
    {
        [Display(Name = "Doğum Günü Kutlaması")] DogumGunu,
        [Display(Name = "Hoşgeldin Mesajı")] Hosgeldin,
        [Display(Name = "İyi Tatiller")] IyiTatiller,
        [Display(Name = "Resmi Tatil Kutlama Mesajı")] ResmiTatil,
        [Display(Name = "Duyuru")] Duyuru,
        [Display(Name = "Diğer")] Diger
    }

}
