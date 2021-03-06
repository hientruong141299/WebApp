using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using WebApp.Common.Configurations;

namespace WebApp.Common.Service
{
    public class HttpClientApi
    {
        private readonly GetConnectApi _getApi;
        private IHttpClientFactory _factory;
        public HttpClientApi(IHttpClientFactory factory,IOptions<GetConnectApi> getApi)
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
