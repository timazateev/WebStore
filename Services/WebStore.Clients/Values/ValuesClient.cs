using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;

namespace WebStore.Clients.Values
{
    public class ValuesClient : BaseClient
    {
        public ValuesClient(IConfiguration Configuration) : base(Configuration, "api/values") { }


    }
}
