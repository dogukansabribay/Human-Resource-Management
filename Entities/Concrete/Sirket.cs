using Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Sirket : BaseEntity
    {
        public Sirket()
        {
            this.Calisanlar = new HashSet<Calisan>();
        }
        public int SirketId { get; set; }
        public string Adi { get; set; }
        public string Logo { get; set; }

        [NotMapped]
        public IFormFile ResimDosyasi { get; set; }
        public ICollection<Calisan> Calisanlar { get; set; }
        public int? SirketUyelikPlaniId { get; set; }
        public SirketUyelikPlani SirketUyelikPlani { get; set; }

        
    }
}
