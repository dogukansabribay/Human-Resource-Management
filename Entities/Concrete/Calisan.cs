using Entities.Abstract;
using Entities.Concrete.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Calisan : BaseEntity
    {
        public Calisan()
        {
            this.Sifreler = new HashSet<Sifre>();
            this.Izinler = new HashSet<Izin>();
            this.CalisanHarcama = new HashSet<CalisanHarcama>();
            this.CalisanKariyer = new HashSet<CalisanKariyer>();
            this.EgitimBilgileri = new HashSet<EgitimBilgi>();
            this.Bildirimler = new HashSet<Bildirim>();
            this.KullaniciYorumlari = new HashSet<KullaniciYorumu>();
        }
        public int CalisanId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DogumTarihi { get; set; }
        [StringLength(11,MinimumLength =11,ErrorMessage ="TC 11 haneli olmalıdır.")]
        public string TcNo { get; set; }
        public ErisimTuru? ErisimTuru { get; set; }
        public SozlesmeTuru? SozlesmeTuru { get; set; }
        public EngelDerecesi? EngelDerecesi { get; set; }
        public string Uyruk { get; set; }
        public Cinsiyet? Cinsiyet { get; set; }
        public KanGrubu? KanGrubu { get; set; }
        public EgitimDurumu? EgitimDurumu { get; set; }
        public EgitimSeviyesi? EgitimSeviyesi { get; set; }
        public string SonTamamlananEgitimKurumu { get; set; }
        public ushort CocukSayisi { get; set; }
        public int? KullandigiYıllıkIzinSayisi { get; set; }
        public int? KalanYıllıkIzinSayisi { get; set; } // bu daha sonra boş geçilemez olmalı
        public int? ToplamKullanilanIzinSayisi { get; set; }
        
        [DataType(DataType.EmailAddress), Required(ErrorMessage ="Lütfen geçerli mail adresi giriniz.")]
        public string MailIs { get; set; }

        [DataType(DataType.EmailAddress), Required(ErrorMessage = "Lütfen geçerli mail adresi giriniz.")]
        public string MailKisisel { get; set; }
        public string TelIs { get; set; }
        public string TelKisisel { get; set; }
        public string Fotograf { get; set; }
        [NotMapped]
        public IFormFile ResimDosyasi { get; set; }
        public bool AktifMi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? IseGirisTarihi { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SozlesmeBitisTarihi { get; set; }
        public MedeniDurum? MedeniDurum { get; set; }
        public DateTime? SonGirişTarihi { get; set; }

        public int? SirketId { get; set; }
        public Sirket Sirket { get; set; }
        public int? AcilDurumBilgiID { get; set; }
        public AcilDurumBilgi AcilDurumBilgi { get; set; }
        public int? AdresId { get; set; }
        public Adres Adres { get; set; }
        public int? BaglantiSosyalMedyaId { get; set; }
        public BaglantiSosyalMedya BaglantiSosyalMedya { get; set; }
        public int? BankaId { get; set; }
        public Banka Banka { get; set; }
        public int? CalisanAvansID { get; set; }

        public ICollection<CalisanAvans> CalisanAvans { get; set; }
        public int? KullaniciYorumuId { get; set; }
        public ICollection<KullaniciYorumu> KullaniciYorumlari { get; set; }

        public ICollection<CalisanKariyer> CalisanKariyer { get; set; }
        public ICollection<CalisanHarcama> CalisanHarcama { get; set; }
        public ICollection<Sifre> Sifreler { get; set; }
        public ICollection<Izin> Izinler { get; set; }
        public ICollection<EgitimBilgi> EgitimBilgileri { get; set; }
        public ICollection<Bildirim> Bildirimler{ get; set; }
    }
}
