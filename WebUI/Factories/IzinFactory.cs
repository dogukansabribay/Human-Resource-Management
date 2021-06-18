using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class IzinFactory
    {
        private readonly HttpClient client;
        private static IzinFactory _factory;
        public static IzinFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new IzinFactory();
                }
                return _factory;
            }
        }

        private IzinFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetIzinler(out IEnumerable<Izin> Izinler)
        {
            var responseTask = client.GetAsync("api/Izin");
            responseTask.Wait();
            var resultTask = responseTask.Result;
            if (resultTask.IsSuccessStatusCode)
            {
                var readTask = resultTask.Content.ReadAsAsync<IEnumerable<Izin>>();
                readTask.Wait();
                Izinler = readTask.Result;
                return resultTask;
            }
            else
            {
                Izinler = Enumerable.Empty<Izin>();
                return resultTask;
            }
        }

        public HttpResponseMessage GetIzinById(int id, out Izin izin)
        {
            var responseTask = client.GetAsync("api/Izin/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Izin>();
                readTask.Wait();
                izin = readTask.Result;
                return result;
            }
            else
            {
                izin = new Izin();
                return result;
            }
        }

        public void AddIzin(Izin izin)
        {
            var postTask = client.PostAsJsonAsync<Izin>("api/Izin", izin);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int id, Izin izin)
        {
            var putTask = client.PutAsJsonAsync<Izin>("api/Izin" + id.ToString(), izin);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }

        public void Sil(Izin izin)
        {
            var deleteTask = client.DeleteAsync("api/Izin/" + izin.IzinID.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }

    }
}
