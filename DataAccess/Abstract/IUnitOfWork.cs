using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IAcilDurumBilgiDal EfAcilDurumBilgiDal { get; }
        IAdresDal EfAdresDal { get;}
        IBaglantiSosyalMedyaDal EfBaglantiSosyalMedyaDal { get; }
        IBankaDal EfBankaDal { get;}
        ICalisanDal EfCalisanDal { get; }
        ICalisanHarcamaDal EfCalisanHarcamaDal { get;}
        ICalisanKariyerDal EfCalisanKariyerDal { get;  }
        IIzinDal EfIzinDal { get;  }
        IKullaniciYorumuDal EfKullaniciYorumuDal { get; }
        IResmiTatilDal EfResmiTatilDal { get; }
        ISifreDal EfSifreDal { get; }
        ISirketUyelikPlaniDal EfSirketUyelikPlaniDal { get;}
       
        IUyelikPlaniDal EfUyelikPlaniDal { get; }
        int Complete();

    }
}
