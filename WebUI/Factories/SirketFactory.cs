using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class SirketFactory
    {
        private readonly HttpClient client;
        private static SirketFactory _factory;

        public static SirketFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new SirketFactory();
                }
                return _factory;
            }
        }
        private SirketFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public void AddSirket(Sirket sirket) 
        {
            var postTask = client.PostAsJsonAsync<Sirket>("api/Sirket", sirket);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode) 
            {
                throw new Exception(result.ReasonPhrase);
            }
        }
        public HttpResponseMessage GetSirketler(out IEnumerable<Sirket> Sirketler)
        {
            var responseTask = client.GetAsync("api/Sirket");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<Sirket>>();
                readTask.Wait();
                Sirketler = readTask.Result;
                return result;
            }
            else
            {
                Sirketler = Enumerable.Empty<Sirket>();
                return result;
            }
        }

        public HttpResponseMessage GetSirketById(int id, out Sirket sirket)
        {
            var responseTask = client.GetAsync("api/Sirket/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Sirket>();
                readTask.Wait();
                sirket = readTask.Result;
                return result;
            }
            else
            {
                sirket = new Sirket();
                return result;
            }
        }
             

        public void Guncelle(int Id, Sirket sirket)
        {
            if (Id != sirket.SirketId) throw new Exception("Şirket id değiştirilemez.");
            var putTask = client.PutAsJsonAsync<Sirket>("api/Sirket/" + Id.ToString(), sirket);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }
        public void Sil(int Id)
        {
            var deleteTast = client.DeleteAsync("api/Sirket/" + Id.ToString());
            deleteTast.Wait();
            var result = deleteTast.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
