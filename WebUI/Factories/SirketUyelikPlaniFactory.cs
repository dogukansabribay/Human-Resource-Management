using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class SirketUyelikPlaniFactory
    {
        private readonly HttpClient client;
        private static SirketUyelikPlaniFactory _factory;
        public static SirketUyelikPlaniFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new SirketUyelikPlaniFactory();
                }
                return _factory;
            }
        }

        private SirketUyelikPlaniFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetPlanlar(out IEnumerable<SirketUyelikPlani> Planlar)
        {
            var responseTask = client.GetAsync("api/SirketUyelikPlani");
            responseTask.Wait();
            var resultTask = responseTask.Result;
            if (resultTask.IsSuccessStatusCode)
            {
                var readTask = resultTask.Content.ReadAsAsync<IEnumerable<SirketUyelikPlani>>();
                readTask.Wait();
                Planlar = readTask.Result;
                return resultTask;
            }
            else
            {
                Planlar = Enumerable.Empty<SirketUyelikPlani>();
                return resultTask;
            }
        }

        public HttpResponseMessage GetPlanById(int id, out SirketUyelikPlani plan)
        {
            var responseTask = client.GetAsync("api/SirketUyelikPlani/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<SirketUyelikPlani>();
                readTask.Wait();
                plan = readTask.Result;
                return result;
            }
            else
            {
                plan = new SirketUyelikPlani();
                return result;
            }
        }

        public void AddPlan(SirketUyelikPlani plan)
        {
            var postTask = client.PostAsJsonAsync<SirketUyelikPlani>("api/SirketUyelikPlani", plan);
            postTask.Wait();
            var result = postTask.Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }
        }

        public void Guncelle(int id, SirketUyelikPlani plan)
        {
            var putTask = client.PutAsJsonAsync<SirketUyelikPlani>("api/SirketUyelikPlani/" + id.ToString(), plan);
            putTask.Wait();
            var result = putTask.Result;
            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ReasonPhrase);
        }

        public void Sil(SirketUyelikPlani plan)
        {
            var deleteTask = client.DeleteAsync("api/SirketUyelikPlani/" + plan.SirketUyelikPlaniID.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (!result.IsSuccessStatusCode) throw new Exception(result.ReasonPhrase);
        }
    }
}
