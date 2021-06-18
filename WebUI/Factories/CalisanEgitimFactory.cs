using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class CalisanEgitimFactory
    {
        private readonly HttpClient client;
        private static CalisanEgitimFactory _factory;
        public static CalisanEgitimFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new CalisanEgitimFactory();
                }
                return _factory;
            }
        }

        private CalisanEgitimFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetEgitimBilgileri(out IEnumerable<EgitimBilgi> EgitimBilgileri)
        {
            var responseTask = client.GetAsync("api/CalisanEgitim");
            responseTask.Wait();
            var resultTask = responseTask.Result;
            if (resultTask.IsSuccessStatusCode)
            {
                var readTask = resultTask.Content.ReadAsAsync<IEnumerable<EgitimBilgi>>();
                readTask.Wait();
                EgitimBilgileri = readTask.Result;
                return resultTask;
            }
            else
            {
                EgitimBilgileri = Enumerable.Empty<EgitimBilgi>();
                return resultTask;
            }
        }

        public HttpResponseMessage GetEgitimBilgisiById(int id, out EgitimBilgi egitimBilgi)
        {
            var responseTask = client.GetAsync("api/CalisanEgitim/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<EgitimBilgi>();
                readTask.Wait();
                egitimBilgi = readTask.Result;
                return result;
            }
            else
            {
                egitimBilgi = new EgitimBilgi();
                return result;
            }
        }

        public void AddEgitimBilgi(EgitimBilgi egitimBilgi)
        {
            var postTask = client.PostAsJsonAsync<EgitimBilgi>("api/CalisanEgitim", egitimBilgi);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int id, EgitimBilgi egitimBilgi)
        {
            var putTask = client.PutAsJsonAsync<EgitimBilgi>("api/CalisanEgitim/" + id.ToString(), egitimBilgi);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }

        public void Sil(EgitimBilgi egitimBilgi)
        {
            var deleteTask = client.DeleteAsync("api/CalisanEgitim/" + egitimBilgi.EgitimBilgiId.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
