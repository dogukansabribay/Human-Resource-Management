using DataAccess.Concrete.Mappings;
using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class KolayIkContext : DbContext
    {
        public KolayIkContext(DbContextOptions<KolayIkContext> options) : base(options)
        {

        }


        public DbSet<AcilDurumBilgi> AcilDurumBilgileri { get; set; }
        public DbSet<Adres> Adresler { get; set; }
        public DbSet<BaglantiSosyalMedya> BaglantiSosyalMedyalar { get; set; }
        public DbSet<Banka> Bankalar { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<CalisanHarcama> CalisanHarcamalari { get; set; }
        public DbSet<CalisanKariyer> CalisanKariyerler { get; set; }
        public DbSet<Izin> Izinler { get; set; }
        public DbSet<KullaniciYorumu> KullaniciYorumlari { get; set; }
        public DbSet<ResmiTatil> ResmiTatiller { get; set; }
        public DbSet<Sifre> Sifreler { get; set; }
        public DbSet<Sirket> Sirketler { get; set; }
        public DbSet<SirketUyelikPlani> SirketUyelikPlanlari { get; set; }
        public DbSet<UyelikPlani> UyelikPlanilari { get; set; }

        public DbSet<CalisanAvans> CalisanAvanslar { get; set; }

        public DbSet<EgitimBilgi> EgitimBilgileri { get; set; }

        public DbSet<CalisanEgitimViewModel> CalisanEgitim { get; set; }
        public DbSet<LoginViewModel> Logins { get; set; }
        public DbSet<Bildirim> Bildirimler { get; set; }

        public DbSet<TarihSecimi> TarihSecimleri { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AcilDurumBilgiMapping());
            modelBuilder.ApplyConfiguration(new AdresMapping());
            modelBuilder.ApplyConfiguration(new BaglantiSosyalMedyaMapping());
            modelBuilder.ApplyConfiguration(new BankaMapping());
            modelBuilder.ApplyConfiguration(new CalisanHarcamaMapping());
            modelBuilder.ApplyConfiguration(new CalisanKariyerMapping());
            modelBuilder.ApplyConfiguration(new CalisanMapping());
            modelBuilder.ApplyConfiguration(new IzinMapping());
            modelBuilder.ApplyConfiguration(new KullaniciYorumuMapping());
            modelBuilder.ApplyConfiguration(new ResmiTatilMapping());
            modelBuilder.ApplyConfiguration(new SifreMapping());
            modelBuilder.ApplyConfiguration(new SirketUyelikPlaniMapping());
            modelBuilder.ApplyConfiguration(new SirketMapping());
            modelBuilder.ApplyConfiguration(new UyelikPlaniMapping());
            modelBuilder.ApplyConfiguration(new EgitimBilgiMapping());
            modelBuilder.ApplyConfiguration(new CalisanAvansMapping());
            modelBuilder.ApplyConfiguration(new BildirimMapping());
            modelBuilder.ApplyConfiguration(new TarihSecimiMapping());

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=DboKolayIk;Trusted_Connection=True;");
        //}
    }
}
