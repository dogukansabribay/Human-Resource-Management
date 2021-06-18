using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfKullaniciYorumuDal : EfEntityRepositoryDal<KullaniciYorumu> , IKullaniciYorumuDal
    {
        public EfKullaniciYorumuDal(KolayIkContext context) : base(context)
        {

        }

        public bool YorumKontrol(KullaniciYorumu yorum)
        {
            List<KullaniciYorumu> yorums = GetAll(a=> a.CalisanId==yorum.CalisanId);
            if (yorums!=null)
            {
                foreach (var item in yorums)
                {
                    if (item.YorumTanimi==yorum.YorumTanimi)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
