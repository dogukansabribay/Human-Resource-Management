using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete.Enums
{
    public enum IzinTanimi
    {
        [Display(Name = "Yıllık İzin")] Yillik,
        [Display(Name = "Evlilik İzni")] Evlilik,
        [Display(Name = "Babalık İzni")] Babalik,
        [Display(Name = "Doğum İzni")] Dogum,
        [Display(Name = "Doğum Sonrası İzni")] DogumSonrasi,
        [Display(Name = "Süt İzni")] Sütİzni,
        [Display(Name = "Askerlik İzni")] Askerlik,
        [Display(Name = "Hastalık İzni")] Hastalik,
        [Display(Name = "İş Arama İzni")] İsArama,
        [Display(Name = "Yol İzni")] Yolİzni
    }

}
