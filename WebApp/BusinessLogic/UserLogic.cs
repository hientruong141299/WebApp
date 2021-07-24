using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;
using WebApp.Common.ConnectionApi;
using WebApp.Common.Constants;
using WebApp.Interfaces;
using WebApp.Services;

namespace WebApp.BusinessLogic
{
    public class UserLogic: IUserLogic
    {
        private IApiService _apiService;
       
        public UserLogic(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<User>> GetAllUsers()
        { 
            var response = await _apiService.GetApi(Constants.GetUserPath);
            var model = JsonConvert.DeserializeObject<List<User>>(response);
            return model;
        }

        public async Task<UserAzureAdd> GetUserAddByEmail(string email)
        {
            var response = await _apiService.GetApi($"/api/users/aad/{email}");
            var model = JsonConvert.DeserializeObject<UserAzureAdd>(response);
            return model;
        }

        public async Task<User> GetUserSqlById(int id)
        {
            var response = await _apiService.GetApi($"/api/users/{id}");
            var model = JsonConvert.DeserializeObject<User>(response);
            return model;
        }
    }
}
