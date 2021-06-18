using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSirketDal : EfEntityRepositoryDal<Sirket>, ISirketDal
    {
        private readonly KolayIkContext context;
        public EfSirketDal(KolayIkContext context):base(context)
        {
            this.context = context;
        }


    }
}
