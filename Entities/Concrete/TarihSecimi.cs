using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class TarihSecimi:BaseEntity
    {
        public int Id { get; set; }
        public DateTime BaslangicTarihi { get; set; }

        public DateTime BitisTarihi { get; set; }
    }
}
