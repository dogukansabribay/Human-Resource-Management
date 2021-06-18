using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class CalisanFactory
    {
        private readonly HttpClient client;
        private static CalisanFactory _factory;
        public static CalisanFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new CalisanFactory();
                }
                return _factory;
            }
        }
        private CalisanFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetCalisanlar(out IEnumerable<Calisan> Calisanlar)
        {
            var responseTask = client.GetAsync("api/Calisan");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<Calisan>>();
                readTask.Wait();
                Calisanlar = readTask.Result;
                return result;
            }
            else
            {
                Calisanlar = Enumerable.Empty<Calisan>();
                return result;
            }
        }

        public HttpResponseMessage GetCalisanById(int id, out Calisan calisan)
        {
            var responseTask = client.GetAsync("api/Calisan/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Calisan>();
                readTask.Wait();
                calisan = readTask.Result;
                return result;
            }
            else
            {
                calisan = new Calisan();
                return result;
            }
        }

        public void AddCalisan(Calisan calisan)
        {
            var postTask = client.PostAsJsonAsync<Calisan>("api/Calisan", calisan);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int Id, Calisan calisan)
        {
            if (Id != calisan.CalisanId) throw new Exception("Çalışan id değiştirilemez.");
            var putTask = client.PutAsJsonAsync<Calisan>("api/calisan/" + Id.ToString(), calisan);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }
        public void Sil(int Id)
        {
            var deleteTast = client.DeleteAsync("api/calisan/" + Id.ToString());
            deleteTast.Wait();
            var result = deleteTast.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
