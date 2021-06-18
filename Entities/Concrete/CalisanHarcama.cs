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
    public class CalisanHarcama : BaseEntity
    {
        public int CalisanHarcamaID { get; set; }

        public string HarcamaAciklamasi { get; set; }

        public string RedAcıklaması { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public double HarcamaMiktari { get; set; }
        public DateTime HarcamaTarihi { get; set; }

        public string HarcamaBelgesiYolu { get; set; }

        [NotMapped]
        public IFormFile HarcamaBelgesi { get; set; }

        public int CalisanId { get; set; }

        public Calisan Calisan { get; set; }

        public OnayDurumu OnayDurumu { get; set; }

    }
}
