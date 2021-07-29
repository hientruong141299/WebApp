using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
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
            var path = string.Format(Constants.GetUserADD, email);           
            var response = await _apiService.GetApi(path);
            var model = JsonConvert.DeserializeObject<UserAzureAdd>(response);
            return model;
        }

        public async Task<User> GetUserSqlById(int id)
        {
            var path = string.Format(Constants.GetUserSqlById, id);
            var response = await _apiService.GetApi(path);
            var model = JsonConvert.DeserializeObject<User>(response);
            return model;
        }
    }
}
