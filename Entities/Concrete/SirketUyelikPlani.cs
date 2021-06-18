using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SirketUyelikPlani : BaseEntity
    {
        public int? SirketUyelikPlaniID { get; set; }
        public int UyelikPlaniID { get; set; }

        public UyelikPlani UyelikPlani { get; set; }

        public int SirketID { get; set; }

        public Sirket Sirket { get; set; }

        public DateTime BaslangicTarihi { get; set; }

        public DateTime? BitisTarihi { get; set; }   

        public bool IsActive { get; set; }

    }
}
