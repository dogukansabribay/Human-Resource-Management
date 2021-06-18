using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.ViewModels
{
    public class CalisanEgitimViewModel
    {
        [Key]
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
        public List<EgitimBilgi> EgitimBilgileri { get; set; }

    }
}
