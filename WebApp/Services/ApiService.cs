using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Common.Configurations;

namespace WebApp.Services
{
    public class ApiService : IApiService
    {
        private readonly GetConnectApi _getApi;
        private IHttpClientFactory _factory;
        private readonly GetAuthToken _getAuthToken;
        public ApiService(IHttpClientFactory factory, IOptions<GetConnectApi> getApi,IOptions<GetAuthToken> getAuthToken)
        {
            _factory = factory;
            _getApi = getApi.Value;
            _getAuthToken = getAuthToken.Value;
        }

        public async Task<string> GetApi(string url)
        {
            HttpClient httpClient = _factory.CreateClient();
            httpClient.BaseAddress = new Uri(_getApi.UrlApi);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("Guid",_getAuthToken.Guid);
            var response = await httpClient.GetAsync(url); 
            string data = await response.Content.ReadAsStringAsync();
            return data;
        }

        //public async Task<string> GetApiUserAdd(string url)
        //{
        //    HttpClient httpClient = _factory.CreateClient();
        //    httpClient.BaseAddress = new Uri(_getApi.UrlApi);
        //    var response = await httpClient.GetAsync(url);
        //    string data = await response.Content.ReadAsStringAsync();
        //    return data;
        //}
    }
}
