using DataAccess.Concrete.EntityFramework;
using DataAccess.ViewModels;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebTestUI.Factories;

namespace WebTestUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly EfCalisanDal EfCalisanDal;
        private readonly EfSifreDal EfSifreDal;
        public LoginController(KolayIkContext context)
        {
            EfCalisanDal = new EfCalisanDal(context);
            EfSifreDal = new EfSifreDal(context);
        }
        public IActionResult Logout()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            //Response.Cookies.Delete("Username");
            //Response.Cookies.Delete("UserId");
            //Response.Cookies.Delete("BildirimSayisi");
            return RedirectToAction("index", "ziyaretci");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginKontrol(LoginViewModel model)
        {
            string EncodedResponse = Request.Form["g-Recaptcha-Response"];
            bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "true" ? true : false);
            if (!IsCaptchaValid)
            {
                ViewBag.Robot = "Git buradan robot!!";
                return View("Index");
            }
            

            Calisan calisan = EfCalisanDal.Get(a => a.MailIs == model.Mail);
            if (calisan == null)
            {
                ViewBag.EmailSifreHatali = "E-Posta adresi veya şifre hatalı";
                return View("Index");
            }
            Sifre sifre = EfSifreDal.Get(a => a.CalisanId == calisan.CalisanId);
            if (sifre.Parola == model.Password && sifre.Calisan.MailIs == model.Mail)
            {
                UserIdCookie(calisan.CalisanId);
                UserNameCookie(calisan.Adi, calisan.Soyadi);
                if (calisan.ErisimTuru==Entities.Concrete.Enums.ErisimTuru.HesapSahibi)
                {
                    return RedirectToAction("index", "Home", new { Area = "SiteYonetici" });
                }
                SirkerAdveLogoCookie((int)calisan.SirketId);
                if (calisan.SonGirişTarihi==null)
                {
                    calisan.SonGirişTarihi = DateTime.Now;
                    calisan.Sifreler = null;
                    CalisanFactory.Factory.Guncelle(calisan.CalisanId, calisan);
                    return RedirectToAction("ParolaDegistirme", "Login");
                }
                if (calisan.ErisimTuru == Entities.Concrete.Enums.ErisimTuru.Calisan)
                {
                    return RedirectToAction("index", "home");
                }
                else if (calisan.ErisimTuru == Entities.Concrete.Enums.ErisimTuru.Yonetici)
                {
                    return RedirectToAction("index","Home",new { Area = "SirketYonetici"});
                }
                return Content("Bişeyler Ters gitti");

            }
            else
            {
                ViewBag.EmailSifreHatali = "E-Posta adresi veya şifre hatalı";
                return View("Index");
            }
        }

       

        public class ReCaptchaClass
        {
            public static string Validate(string EncodedResponse)
            {
                var client = new System.Net.WebClient();

                string PrivateKey = "6LdCBdAaAAAAAPcEo3R3oFIhMBFgy4TXVGMraTVz";

                var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));

                var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);

                return captchaResponse.Success.ToLower();
            }

            [JsonProperty("success")]
            public string Success
            {
                get { return m_Success; }
                set { m_Success = value; }
            }

            private string m_Success;
            [JsonProperty("error-codes")]
            public List<string> ErrorCodes
            {
                get { return m_ErrorCodes; }
                set { m_ErrorCodes = value; }
            }


            private List<string> m_ErrorCodes;
        }
        private void UserNameCookie(string adi, string soyadi)
        {
            if (Request.Cookies["Username"] == null)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(20)
                };
                Response.Cookies.Append("Username", adi + " " + soyadi, options);
            }
        }
        private void SirkerAdveLogoCookie(int sirketId)
        {
            Sirket sirket;
            SirketFactory.Factory.GetSirketById(sirketId, out sirket);
            if (Request.Cookies["SirketAd"] == null && Request.Cookies["SirketLogo"] == null)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(20)
                };
                Response.Cookies.Append("SirketAd", sirket.Adi, options);
                Response.Cookies.Append("SirketLogo", sirket.Logo, options);
            }

        }
        private void DeleteUserNameCookie()
        {
            if (Request.Cookies["Username"] != null)
            {
                Response.Cookies.Delete("Username");
            }
        }
        private void UserIdCookie(int calisanId)
        {
            if (Request.Cookies["UserId"] == null)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(20)
                };
                Response.Cookies.Append("UserId", calisanId.ToString(), options);
            }

        }
        private void DeleteUserIdCookie()
        {            
            if (Request.Cookies["UserId"] != null)
            {
                Response.Cookies.Delete("UserId");
            }
        }

        public IActionResult ParolaDegistirme()
        {
            int id = KullaniciId();
            IEnumerable<Sifre> sifreler;
            SifreFactory.Factory.GetSifreler(out sifreler);
            SifreDegistirmeViewModel model = new SifreDegistirmeViewModel
            {
                CalisanId = KullaniciId(),
                //SifreId = EfSifreDal.GetSifreIdByCalisanId(id)
                SifreId = sifreler.Where(a=>a.CalisanId==id).FirstOrDefault().SifreId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ParolaDegistirme(SifreDegistirmeViewModel model)
        {
            Calisan calisan;
            CalisanFactory.Factory.GetCalisanById(KullaniciId(), out calisan);
            model.CalisanId = calisan.CalisanId;
            IEnumerable<Sifre> sifreler;
            SifreFactory.Factory.GetSifreler(out sifreler);
            model.SifreId = sifreler.Where(a => a.CalisanId == calisan.CalisanId).FirstOrDefault().SifreId;
            if (ModelState.IsValid)
            {
                //Sifre yeniSifre = EfSifreDal.GetById(model.SifreId);
                Sifre yeniSifre = sifreler.Where(a=>a.SifreId==model.SifreId).FirstOrDefault();
                if (model.YeniParola == yeniSifre.Parola)
                {
                    ModelState.AddModelError(string.Empty, "Yeni şifreniz eski şifreniz olamaz.");
                    return View(model);
                }
                //if (model.EskiParola != EfSifreDal.GetById(model.SifreId).Parola)
                if (model.EskiParola != yeniSifre.Parola)
                {
                    ModelState.AddModelError(string.Empty, "Eski Şifreyi Hatalı Girdiniz.");
                    return View(model);
                }
                if (model.YeniParola != model.YeniParolaTekrar)
                {
                    ModelState.AddModelError(string.Empty, "Girdiğiniz şifreler uyuşmuyor.");
                    return View(model);
                }
                if (!SifreKontrol(model.YeniParola))
                {
                    ModelState.AddModelError(string.Empty, "Şifreniz en az bir rakam ve büyük harf içermelidir.");
                    return View(model);
                }
                DeleteUserNameCookie();
                DeleteUserIdCookie();
                yeniSifre.Parola = model.YeniParola;
                EfSifreDal.Update(yeniSifre);
                SifreFactory.Factory.Guncelle(yeniSifre.SifreId,yeniSifre);
                MailGonder(calisan.MailIs);
                return RedirectToAction("index", "Login");
            }
            ModelState.AddModelError("hata", "Bir sıkıntı oldu");
            return View(model);
        }
        private bool SifreKontrol(string sifre)
        {
            Match password = Regex.Match(sifre, @"
                                      ^              # Match the start of the string
                                       (?=.*\p{Lu})  # Positive lookahead assertion, is true when there is an uppercase letter
                                       (?=.*\P{L})   # Positive lookahead assertion, is true when there is a non-letter
                                       \S{8,}        # At least 8 non whitespace characters
                                      $              # Match the end of the string
                                     ", RegexOptions.IgnorePatternWhitespace);
            return password.Success ? true : false;

        }
        private void MailGonder(string mailIs)
        {
            SmtpClient sc = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = new NetworkCredential("dragunov199137@gmail.com", "741852963Ab")
            };
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("dragunov199137@gmail.com")
            };
            mail.To.Add(mailIs);
            mail.Subject = "Şifreniz değiştirilmiştir.";
            mail.IsBodyHtml = true;
            mail.Body = "IK proje şifreniz değiştirilmiştir. Bu eylemi siz yapmadıysanız yöneticinizle iletişime geçiniz.";
            sc.Send(mail);
        }

        private int KullaniciId()
        {
            if (Request.Cookies["UserId"] == null)
            {
                return 0;
            }
            return int.Parse(Request.Cookies["UserId"]);
        }

    }
}
