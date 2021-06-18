using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfResmiTatilDal : EfEntityRepositoryDal<ResmiTatil> , IResmiTatilDal
    {
        private readonly KolayIkContext _context;
        public EfResmiTatilDal(KolayIkContext context) : base(context)
        {
            _context = context;
        }

        public List<ResmiTatil> ResmiTatiller(Izin izin)
        {
            List<ResmiTatil> tatiller = _context.ResmiTatiller.Where(a=> a.BaslangicTarihi >= DateTime.Now && a.BaslangicTarihi<=izin.MesaiBaslangicTarihi).Select(a=> new ResmiTatil {Aciklama=a.Aciklama,BaslangicTarihi= a.BaslangicTarihi, BitisTarihi=a.BitisTarihi }).ToList();
            return tatiller;
        }

        public List<DateTime> ResmiTatiller()
        {
            throw new NotImplementedException();
        }

        public List<ResmiTatil> BildirimGonderilecekResmiTatilVarMi()
        {
            List<ResmiTatil> resmiTatil = new List<ResmiTatil>();
            int gunSayisi = 0;
            if (DateTime.Now.DayOfWeek==DayOfWeek.Friday)
            {
                gunSayisi = 2;
                for (int i = 0; i < gunSayisi; i++)
                {
                    resmiTatil.AddRange(_context.ResmiTatiller.Where(a => a.BaslangicTarihi == DateTime.Now.AddDays(1)));
                }
            }
            else
            {
                resmiTatil= (List<ResmiTatil>)_context.ResmiTatiller.Where(a=> a.BaslangicTarihi==DateTime.Now.AddDays(1)).ToList();
            }

            if (resmiTatil==null)
            {
                return null;
            }
            else
            {
                return resmiTatil;
            }
        }
    }
}
