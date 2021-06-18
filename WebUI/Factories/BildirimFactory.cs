using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class BildirimFactory
    {
        private readonly HttpClient client;
        private static BildirimFactory _factory;
        public static BildirimFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new BildirimFactory();
                }
                return _factory;
            }
        }

        private BildirimFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetBildirimler(out IEnumerable<Bildirim> Bildirimler)
        {
            var responseTask = client.GetAsync("api/Bildirim");
            responseTask.Wait();
            var resultTask = responseTask.Result;
            if (resultTask.IsSuccessStatusCode)
            {
                var readTask = resultTask.Content.ReadAsAsync<IEnumerable<Bildirim>>();
                readTask.Wait();
                Bildirimler = readTask.Result;
                return resultTask;
            }
            else
            {
                Bildirimler = Enumerable.Empty<Bildirim>();
                return resultTask;
            }
        }

        public HttpResponseMessage GetBildirimById(int id, out Bildirim bildirim)
        {
            var responseTask = client.GetAsync("api/Bildirim/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Bildirim>();
                readTask.Wait();
                bildirim = readTask.Result;
                return result;
            }
            else
            {
                bildirim = new Bildirim();
                return result;
            }
        }

        public void AddBildirim(Bildirim bildirim)
        {
            var postTask = client.PostAsJsonAsync<Bildirim>("api/Bildirim", bildirim);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int id, Bildirim bildirim)
        {
            var putTask = client.PutAsJsonAsync<Bildirim>("api/Bildirim/" + id.ToString(), bildirim);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }

        public void Sil(Bildirim bildirim)
        {
            var deleteTask = client.DeleteAsync("api/Bildirim/" + bildirim.BildirimId.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
