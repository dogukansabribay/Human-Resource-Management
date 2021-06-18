using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTestUI.Factories
{
    public class ResmiTatilFactory
    {
        private readonly HttpClient client;
        private static ResmiTatilFactory _factory;

        public static ResmiTatilFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new ResmiTatilFactory();
                }
                return _factory;
            }
        }

        public ResmiTatilFactory()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:30423/")
            };
        }

        public HttpResponseMessage GetResmiTatiller(out IEnumerable<ResmiTatil> resmiTatiller)
        {
            var responseTask = client.GetAsync("api/ResmiTatil");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<ResmiTatil>>();
                readTask.Wait();
                resmiTatiller = readTask.Result;
                return result;
            }
            else
            {
                resmiTatiller = Enumerable.Empty<ResmiTatil>();
                return result;
            }
        }

        public HttpResponseMessage GetResmiTatilById(int id, out ResmiTatil resmiTatil)
        {
            var responseTask = client.GetAsync("api/ResmiTatil" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ResmiTatil>();
                readTask.Wait();
                resmiTatil = readTask.Result;
                return result;
            }
            else
            {
                resmiTatil = new ResmiTatil();
                return result;
            }
        }

        //EKLE-GÜNCELLE-SİL DAHA SONRA EKLENEBİLİR-Mİ?

    }
}
