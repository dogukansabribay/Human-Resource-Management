using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Entities.Concrete.Enums;
namespace DataAccess.Abstract
{
    public interface ICalisanAvansDal : IEntityRepository<CalisanAvans>
    {
        void AvansBildirim(string Email)
        {

            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("dragunov199137@gmail.com", "741852963Ab");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dragunov199137@gmail.com");
            mail.To.Add(Email);
            mail.Subject = "Avans Talebi";
            mail.IsBodyHtml = true;
            mail.Body = "Avans talebiniz alınmıştır.";
            sc.Send(mail);


        }

        CalisanAvans DefaultAyarlar(CalisanAvans calisan)
        {
            if (calisan.TaksitSayısı != null)
            {
                calisan.AvansTalepEdilenTarih = DateTime.Now;
                calisan.OnayDurumu = 
                    OnayDurumu.Belirsiz;
                return calisan;
            }
            else
            {
                calisan.AvansTalepEdilenTarih = DateTime.Now;
                calisan.OnayDurumu = OnayDurumu.Belirsiz;
                calisan.TaksitSayısı = 1;
                return calisan;
            }
        }
        DateTime AvansTalepTarihKontrol(DateTime date)
        {
            if (date > DateTime.Now)
            { return date; }
            else { throw new Exception("Avans tarihi geçmiş tarihli olamaz!!"); }
        }

        void OnaylananAvans(CalisanAvans avans) 
        {
            avans.OnayDurumu = OnayDurumu.Onaylı;
            avans.AvansOnaylandıgıTarih = DateTime.Now;
            
            
        }
        
    }
}
