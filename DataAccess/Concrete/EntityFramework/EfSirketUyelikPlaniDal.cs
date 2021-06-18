using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSirketUyelikPlaniDal : EfEntityRepositoryDal<SirketUyelikPlani> , ISirketUyelikPlaniDal
    {
        private readonly KolayIkContext _context;
        public EfSirketUyelikPlaniDal(KolayIkContext context) : base(context)
        {
            _context = context;
        }

    }
}
