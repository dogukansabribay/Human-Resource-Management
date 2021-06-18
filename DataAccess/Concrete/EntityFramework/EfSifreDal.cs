using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSifreDal : EfEntityRepositoryDal<Sifre> , ISifreDal
    {
        private readonly KolayIkContext context;
        public EfSifreDal(KolayIkContext context) : base(context)
        {
            this.context = context;
        }

        public int GetSifreIdByCalisanId(int id)
        {
            return context.Sifreler.Where(a => a.CalisanId == id).Select(a => a.SifreId).FirstOrDefault();
        }
    }
}
