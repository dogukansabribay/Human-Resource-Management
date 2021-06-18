using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class UyelikPlani : BaseEntity
    {
        public UyelikPlani()
        {
            this.SirketUyelikPlani = new HashSet<SirketUyelikPlani>();
        }
        public int UyelikPlaniID { get; set; }

        public string UyelikPlaniTanimi { get; set; }
        public int CalisanSayisi { get; set; }

        public double PlanUcreti { get; set; }
  
        public ICollection<SirketUyelikPlani> SirketUyelikPlani { get; set; }
       
        
    }
}
