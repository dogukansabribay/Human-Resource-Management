using Entities.Abstract;
using Entities.Concrete.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Izin : BaseEntity
    {
        public int IzinID { get; set; }

        public IzinTanimi IzinTanimi { get; set; }

        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }

        [DataType(DataType.Date)]
        public DateTime MesaiBaslangicTarihi { get; set; }

        public OnayDurumu OnayDurumu { get; set; }

        public string IzinDetayTalebi { get; set; }

        public string IzinBelgesiYolu { get; set; }

        [NotMapped]
        public IFormFile IzinBelgesi { get; set; }

        public string RedAcıklaması { get; set; }
        public int IzinKullanilanGunSayisi { get; set; }

        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }

    }



}
