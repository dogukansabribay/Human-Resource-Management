using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class SifreDegistirmeViewModel
    {
        [Key]
        public int SifreId { get; set; }
        public int CalisanId { get; set; }
        public string EskiParola { get; set; }
        public string YeniParola { get; set; }
        public string YeniParolaTekrar { get; set; }
    }
}
