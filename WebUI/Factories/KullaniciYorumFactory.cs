using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class KullaniciYorumFactory
    {
        private readonly HttpClient client;
        private static KullaniciYorumFactory _factory;
        public static KullaniciYorumFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new KullaniciYorumFactory();
                }
                return _factory;
            }
        }

        private KullaniciYorumFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetYorumlar(out IEnumerable<KullaniciYorumu> kullaniciYorumlari)
        {
            var responseTask = client.GetAsync("api/KullaniciYorumu");
            responseTask.Wait();
            var resultTask = responseTask.Result;
            if (resultTask.IsSuccessStatusCode)
            {
                var readTask = resultTask.Content.ReadAsAsync<IEnumerable<KullaniciYorumu>>();
                readTask.Wait();
                kullaniciYorumlari = readTask.Result;
                return resultTask;
            }
            else
            {
                kullaniciYorumlari = Enumerable.Empty<KullaniciYorumu>();
                return resultTask;
            }
        }

        public HttpResponseMessage GetYorumById(int id, out KullaniciYorumu kullaniciYorum)
        {
            var responseTask = client.GetAsync("api/KullaniciYorumu/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<KullaniciYorumu>();
                readTask.Wait();
                kullaniciYorum = readTask.Result;
                return result;
            }
            else
            {
                kullaniciYorum = new KullaniciYorumu();
                return result;
            }
        }

        public void AddYorum(KullaniciYorumu kullaniciYorum)
        {
            var postTask = client.PostAsJsonAsync<KullaniciYorumu>("api/KullaniciYorumu", kullaniciYorum);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int id, KullaniciYorumu kullaniciYorum)
        {
            var putTask = client.PutAsJsonAsync<KullaniciYorumu>("api/KullaniciYorumu/" + id.ToString(), kullaniciYorum);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }

        public void Sil(KullaniciYorumu kullaniciYorum)
        {
            var deleteTask = client.DeleteAsync("api/KullaniciYorumu/" + kullaniciYorum.KullaniciYorumuId.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
