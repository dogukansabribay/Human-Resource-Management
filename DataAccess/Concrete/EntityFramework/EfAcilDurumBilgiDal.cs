using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAcilDurumBilgiDal :EfEntityRepositoryDal<AcilDurumBilgi> , IAcilDurumBilgiDal
    {
        public EfAcilDurumBilgiDal(KolayIkContext context):base(context)
        {

        }
    }
}
