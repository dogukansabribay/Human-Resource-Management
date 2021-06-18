using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class UyelikPlaniFactory
    {
        private readonly HttpClient client;
        private static UyelikPlaniFactory _factory;

        public static UyelikPlaniFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new UyelikPlaniFactory();
                }
                return _factory;
            }
        }

        private UyelikPlaniFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetUyelikPlanlari(out IEnumerable<UyelikPlani> UyelikPlanlari)
        {
            var responseTask = client.GetAsync("api/UyelikPlani");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<UyelikPlani>>();
                readTask.Wait();
                UyelikPlanlari = readTask.Result;
                return result;
            }
            else
            {
                UyelikPlanlari = Enumerable.Empty<UyelikPlani>();
                return result;
            }
        }

        public HttpResponseMessage GetUyelikPlaniById(int id, out UyelikPlani UyelikPlani)
        {
            var responseTask = client.GetAsync("api/UyelikPlani/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<UyelikPlani>();
                readTask.Wait();
                UyelikPlani = readTask.Result;
                return result;
            }
            else
            {
                UyelikPlani = new UyelikPlani();
                return result;
            }
        }

        public void UyelikPlaniEkle(UyelikPlani UyelikPlani)
        {
            var postTask = client.PostAsJsonAsync<UyelikPlani>("api/UyelikPlani", UyelikPlani);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int Id, UyelikPlani UyelikPlani)
        {
            if (Id != UyelikPlani.UyelikPlaniID) throw new Exception("id değiştirilemez.");
            var putTask = client.PutAsJsonAsync<UyelikPlani>("api/UyelikPlani/" + Id.ToString(), UyelikPlani);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }
        public void Sil(int Id)
        {
            var deleteTast = client.DeleteAsync("api/UyelikPlani/" + Id.ToString());
            deleteTast.Wait();
            var result = deleteTast.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }



    }
}
