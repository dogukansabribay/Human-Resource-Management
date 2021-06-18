using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class SifreFactory
    {
        private readonly HttpClient client;
        private static SifreFactory _factory;
        public static SifreFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new SifreFactory();
                }
                return _factory;
            }
        }

        private SifreFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetSifreler(out IEnumerable<Sifre> Sifreler)
        {
            var responseTask = client.GetAsync("api/Sifre");
            responseTask.Wait();
            var resultTask = responseTask.Result;
            if (resultTask.IsSuccessStatusCode)
            {
                var readTask = resultTask.Content.ReadAsAsync<IEnumerable<Sifre>>();
                readTask.Wait();
                Sifreler = readTask.Result;
                return resultTask;
            }
            else
            {
                Sifreler = Enumerable.Empty<Sifre>();
                return resultTask;
            }
        }

        public HttpResponseMessage GetSifreById(int id, out Sifre sifre)
        {
            var responseTask = client.GetAsync("api/Sifre/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Sifre>();
                readTask.Wait();
                sifre = readTask.Result;
                return result;
            }
            else
            {
                sifre = new Sifre();
                return result;
            }
        }

        public void AddSifre(Sifre sifre)
        {
            var postTask = client.PostAsJsonAsync<Sifre>("api/Sifre", sifre);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int id, Sifre sifre)
        {
            var putTask = client.PutAsJsonAsync<Sifre>("api/Sifre/" + id.ToString(), sifre);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }

        public void Sil(Sifre sifre)
        {
            var deleteTask = client.DeleteAsync("api/Sifre/" + sifre.SifreId.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
