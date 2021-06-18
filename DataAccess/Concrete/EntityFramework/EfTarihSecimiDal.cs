using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTarihSecimiDal:EfEntityRepositoryDal<TarihSecimi>, ITarihSecimiDal
    {
        public EfTarihSecimiDal(KolayIkContext context):base(context)
        {

        }
    }
}
