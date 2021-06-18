using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfIzinDal : EfEntityRepositoryDal<Izin>, IIzinDal
    {
        private readonly KolayIkContext _context;
        private readonly EfResmiTatilDal efResmiTatilDal;
        public EfIzinDal(KolayIkContext context) : base(context)
        {
            efResmiTatilDal = new EfResmiTatilDal(context);
            _context = context;
        }
        public List<Izin> Izinler()
        {
            throw new NotImplementedException();
        }

        public int KullanilacakgunSayisiHesapla(Izin izin)
        {
            DateTime date = izin.BaslangicTarihi;
            List<ResmiTatil> tatiller = efResmiTatilDal.ResmiTatiller(izin);
            int resmiTatilIcerenGunSayisi = 0;
            for (DateTime i = date; i <= izin.MesaiBaslangicTarihi; i = i.AddDays(1))
            {
                if (i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
                {
                    resmiTatilIcerenGunSayisi++;
                }
                else
                {
                    foreach (var item in tatiller)
                    {
                        for (DateTime x = item.BaslangicTarihi; x < item.BitisTarihi; x = x.AddDays(1))
                        {
                            if (i == x)
                            {
                                resmiTatilIcerenGunSayisi++;
                            }
                        }
                    }
                }
            }

            int tarihlerArasıSure = (izin.MesaiBaslangicTarihi - izin.BaslangicTarihi).Days;
            return tarihlerArasıSure - resmiTatilIcerenGunSayisi;
        }

        public bool IzinTarihleriKarsilastir(Izin izin)
        {
            List<Izin> calisaninIzinleri = GetAll(a => a.CalisanId == izin.CalisanId);
            foreach (var item in calisaninIzinleri)
            {
                for (DateTime x = izin.BaslangicTarihi; x < izin.MesaiBaslangicTarihi; x = x.AddDays(1))
                {
                    for (DateTime i = item.BaslangicTarihi; i < item.MesaiBaslangicTarihi; i = i.AddDays(1))
                    {
                        if (i == x)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool MesaiBaslangicHaftasonuVeyaResmiTatilKontol(Izin izin)
        {
            List<ResmiTatil> tatiller = efResmiTatilDal.ResmiTatiller(izin);
            foreach (var item in tatiller)
            {
                for (DateTime i = item.BaslangicTarihi; i < item.BitisTarihi; i = i.AddDays(1))
                {
                    if (i == izin.MesaiBaslangicTarihi)
                    {
                        return false;
                    }
                }
            }
            if (izin.MesaiBaslangicTarihi.DayOfWeek == DayOfWeek.Saturday || izin.MesaiBaslangicTarihi.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            return true;
        }

        public bool IzinBaslangicTarihiBugundenOnceMi(Izin izin)
        {
            if (izin.BaslangicTarihi <= DateTime.Now)
            {
                return false;
            }
            return true;
        }
        public bool MesaiBaslangicTarihiIzinBaslangicTarihindenOnceMi(Izin izin)
        {
            if (izin.MesaiBaslangicTarihi <= izin.BaslangicTarihi)
            {
                return false;

            }
            return true;
        }


        public List<Izin> IzınBildirimiGonder()
        {
            List<Izin> calisanIzin = new List<Izin>();
            ResmiTatil resmiTatil = efResmiTatilDal.Get(a => a.BaslangicTarihi.Day == DateTime.Now.AddDays(1).Day && a.BaslangicTarihi.Month == DateTime.Now.Month && a.BaslangicTarihi.Year == DateTime.Now.Year);

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
                    calisanIzin.AddRange(_context.Izinler.Where(a => a.BaslangicTarihi.Month == DateTime.Now.Month && a.BaslangicTarihi.Day == DateTime.Now.AddDays(i).Day).ToList());
                }
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                calisanIzin = _context.Izinler.Where(a => a.BaslangicTarihi.Month == DateTime.Now.Month && (a.BaslangicTarihi.Day == DateTime.Now.Day || a.BaslangicTarihi.Day == DateTime.Now.AddDays(1).Day || a.BaslangicTarihi.Day == DateTime.Now.AddDays(2).Day)&& a.OnayDurumu==OnayDurumu.Onaylı).ToList();
            }



            if (calisanIzin == null)
            {
                return null;
            }
            else
            {
                return calisanIzin;
            }
        }
    }
}
