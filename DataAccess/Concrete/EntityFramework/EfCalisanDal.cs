using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCalisanDal : EfEntityRepositoryDal<Calisan>, ICalisanDal
    {
        private readonly KolayIkContext _context;
        private readonly EfResmiTatilDal _efREsmiTatilDal;
        public EfCalisanDal(KolayIkContext context) : base(context)
        {
            _context = context;
            _efREsmiTatilDal = new EfResmiTatilDal(context);
        }

        public List<Calisan> DogumGunuKutlanacakCalisanlar()
        {
            List<Calisan> calisans = new List<Calisan>();

            ResmiTatil resmiTatil = _efREsmiTatilDal.Get(a => a.BaslangicTarihi.Day == DateTime.Now.AddDays(1).Day && a.BaslangicTarihi.Month == DateTime.Now.Month && a.BaslangicTarihi.Year == DateTime.Now.Year);
            if (resmiTatil != null)
            {
                int gunSayisi = 0;

                gunSayisi = resmiTatil.BitisTarihi.Day - resmiTatil.BaslangicTarihi.Day;
                if (DateTime.Now.AddDays(gunSayisi + 1).DayOfWeek == DayOfWeek.Saturday)
                {
                    gunSayisi += 2;
                }
                else if (DateTime.Now.AddDays(gunSayisi + 1).DayOfWeek == DayOfWeek.Sunday)
                {
                    gunSayisi += 1;
                }

                for (int i = 0; i < gunSayisi; i++)
                {
                   calisans.AddRange( _context.Calisanlar.Where(a => a.DogumTarihi.Value.Month == DateTime.Now.Month && a.DogumTarihi.Value.Day == DateTime.Now.AddDays(i).Day && a.AktifMi).ToList());
                }
            }

            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                calisans = _context.Calisanlar.Where(a => a.DogumTarihi.Value.Month == DateTime.Now.Month && (a.DogumTarihi.Value.Day == DateTime.Now.Day || a.DogumTarihi.Value.Day == DateTime.Now.AddDays(1).Day || a.DogumTarihi.Value.Day == DateTime.Now.AddDays(2).Day) && a.AktifMi).ToList();
            }



            if (calisans == null)
            {
                return new List<Calisan>();
            }
            else
            {
                return calisans;
            }
        }


    }
}
