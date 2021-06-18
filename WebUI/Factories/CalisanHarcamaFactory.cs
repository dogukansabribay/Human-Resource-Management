using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class CalisanHarcamaFactory
    {
        private readonly HttpClient client;
        private static CalisanHarcamaFactory _factory;
        public static CalisanHarcamaFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new CalisanHarcamaFactory(); 
                }
                return _factory;
            }
        }
        private CalisanHarcamaFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetHarcamalar(out IEnumerable<CalisanHarcama> Harcamalar)
        {
            var responseTask = client.GetAsync("api/CalisanHarcamalar");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<CalisanHarcama>>();
                readTask.Wait();
                Harcamalar = readTask.Result;
                return result;
            }
            else
            {
                Harcamalar = Enumerable.Empty<CalisanHarcama>();
                return result;
            }
        }

        public HttpResponseMessage GetHarcamaById(int id, out CalisanHarcama harcama)
        {
            var responseTask = client.GetAsync("api/CalisanHarcamalar/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<CalisanHarcama>();
                readTask.Wait();
                harcama = readTask.Result;
                return result;
            }
            else
            {
                harcama = new CalisanHarcama();
                return result;
            }
        }

        public void AddHarcama(CalisanHarcama harcama)
        {
            var postTask = client.PostAsJsonAsync<CalisanHarcama>("api/CalisanHarcamalar", harcama);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int Id, CalisanHarcama harcama)
        {
            if (Id != harcama.CalisanHarcamaID) throw new Exception("Çalışan Harcama id değiştirilemez.");
            var putTask = client.PutAsJsonAsync<CalisanHarcama>("api/CalisanHarcamalar/" + Id.ToString(), harcama);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }
        public void Sil(int Id)
        {
            var deleteTast = client.DeleteAsync("api/CalisanHarcamalar/" + Id.ToString());
            deleteTast.Wait();
            var result = deleteTast.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
