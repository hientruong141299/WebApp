using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Common.ConnectionApi;

namespace WebApp.Common.Service
{
    public class ClientApiService : IApiService
    {
        private readonly GetConnectApi _getApi;
        private IHttpClientFactory _factory;
        public ClientApiService(IHttpClientFactory factory, IOptions<GetConnectApi> getApi)
        {
            _factory = factory;
            _getApi = getApi.Value;
        }
        public HttpClient GetClientApi()
        {
            HttpClient httpClient = _factory.CreateClient();
            httpClient.BaseAddress = new Uri(_getApi.UrlApi);
            return httpClient;
        }
    }
}
