using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class CalisanAvansFactory
    {
        private HttpClient client;
        private static CalisanAvansFactory _factory;
        public static CalisanAvansFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new CalisanAvansFactory();
                }
                return _factory;
            }
        }
        private CalisanAvansFactory()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:30423/");
        }
        public HttpResponseMessage GetAvanslar(out IEnumerable<CalisanAvans> Avanslar)
        {
            var responseTask = client.GetAsync("api/CalisanAvans");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<CalisanAvans>>();
                readTask.Wait();
                Avanslar = readTask.Result;
                return result;
            }
            else
            {
                Avanslar = Enumerable.Empty<CalisanAvans>();
                return result;
            }
        }

        public HttpResponseMessage GetAvansById(int id, out CalisanAvans avans)
        {
            var responseTask = client.GetAsync("api/CalisanAvans/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<CalisanAvans>();
                readTask.Wait();
                avans = readTask.Result;
                return result;
            }
            else
            {
                avans = new CalisanAvans();
                return result;
            }
        }

        public void AddAvans(CalisanAvans avans)
        {
            var postTask = client.PostAsJsonAsync<CalisanAvans>("api/CalisanAvans", avans);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int Id, CalisanAvans avans)
        {
            if (Id != avans.CalisanAvansID) throw new Exception("Çalışan avans id değiştirilemez.");
            var putTask = client.PutAsJsonAsync<CalisanAvans>("api/CalisanAvans/" + Id.ToString(), avans);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }
        public void Sil(int Id)
        {
            var deleteTast = client.DeleteAsync("api/CalisanAvans/" + Id.ToString());
            deleteTast.Wait();
            var result = deleteTast.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
