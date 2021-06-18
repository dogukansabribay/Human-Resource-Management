using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBildirimDal : EfEntityRepositoryDal<Bildirim>, IBildirimDal
    {
        public EfBildirimDal(KolayIkContext context) : base(context)
        {
        }

        public bool SablonAktifliginiKarsilastir(Bildirim bildirim)
        {
            List<Bildirim> bildirims = GetAll(a=>a.CalisanId==bildirim.CalisanId);
            if (bildirim.GonderilecekMi == true)
            {
                foreach (var item in bildirims)
                {
                    if (item.GonderilecekMi==true && item.SablonTanimi == bildirim.SablonTanimi)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
