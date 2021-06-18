using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IIzinDal :IEntityRepository<Izin>
    {
        List<Izin> Izinler();
        int KullanilacakgunSayisiHesapla(Izin izin);
    }
}
