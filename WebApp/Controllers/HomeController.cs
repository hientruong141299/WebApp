using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;
using WebApp.Common.ConnectionApi;
using WebApp.Common.Constants;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserLogic _userLogic;
        //private readonly ILogger<HomeController> _logger;
        //private IHttpClientFactory _factory;
        //private readonly HttpClientApi _httpClientApi;
        //private readonly UserApi _userApi;
        //private readonly IApiService _apiService;
        public HomeController(IUserLogic userLogic)
        {
            //_logger = logger;
            //_factory = factory;
            //_httpClientApi = new HttpClientApi(_factory,getApi);
            //_apiService = apiService;
            //_userApi = new UserApi();
            _userLogic = userLogic;

        }
        public async Task<IActionResult>Index()
        {
            var response = await _userLogic.GetAllUsers();

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
