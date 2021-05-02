using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected string Address { get; }

        protected HttpClient Http { get; }

        public BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            Address = ServiceAddress;

            Http = new HttpClient
            {
                BaseAddress = new System.Uri(Configuration["WebApiURL"]),
                DefaultRequestHeaders =
                { 
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json0") }
                }
            };
        }
    }
}
