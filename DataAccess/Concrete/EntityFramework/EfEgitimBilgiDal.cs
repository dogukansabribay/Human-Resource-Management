using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEgitimBilgiDal : EfEntityRepositoryDal<EgitimBilgi>, IEgitimBilgiDal
    {
        public EfEgitimBilgiDal(KolayIkContext context) : base(context)
        {
        }
    }
}
