using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KolayIkContext _context;
        public UnitOfWork(KolayIkContext context)
        {
            _context = context;
            EfAcilDurumBilgiDal = new EfAcilDurumBilgiDal(_context);
            EfAdresDal = new EfAdresDal(_context);
            EfBaglantiSosyalMedyaDal = new EfBaglantiSosyalMedyaDal(_context);
            EfBankaDal = new EfBankaDal(_context);
            EfCalisanDal = new EfCalisanDal(_context);
            EfCalisanHarcamaDal = new EfCalisanHarcamaDal(_context);
            EfCalisanKariyerDal = new EfCalisanKariyerDal(_context);
            EfIzinDal = new EfIzinDal(_context);
            EfKullaniciYorumuDal = new EfKullaniciYorumuDal(_context);
            EfResmiTatilDal = new EfResmiTatilDal(_context);
            EfSifreDal = new EfSifreDal(_context);
            EfSirketUyelikPlaniDal = new EfSirketUyelikPlaniDal(_context);
            EfUyelikPlaniDal = new EfUyelikPlaniDal(_context);
        }
        public IAcilDurumBilgiDal EfAcilDurumBilgiDal { get; private set; }
        public IAdresDal EfAdresDal { get; private set; }
        public IBaglantiSosyalMedyaDal EfBaglantiSosyalMedyaDal { get; private set; }
        public IBankaDal EfBankaDal { get; private set; }
        public ICalisanDal EfCalisanDal { get; private set; }
        public ICalisanHarcamaDal EfCalisanHarcamaDal { get; private set; }
        public ICalisanKariyerDal EfCalisanKariyerDal { get; private set; }
        public IIzinDal EfIzinDal { get; private set; }
        public IKullaniciYorumuDal EfKullaniciYorumuDal { get; private set; }
        public IResmiTatilDal EfResmiTatilDal { get ; private set; }
        public ISifreDal EfSifreDal { get; private set; }
        public ISirketUyelikPlaniDal EfSirketUyelikPlaniDal { get; private set; }
        public IUyelikPlaniDal EfUyelikPlaniDal { get; private set; }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
