using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.BuisinessLogic
{
    public class UserAzureADLogic : IUserADLogic
    {
        private readonly IGraphApiServices _graphApiServices;
        public UserAzureADLogic(IGraphApiServices graphApiServices)
        {
            _graphApiServices = graphApiServices;
        }
        public async Task<UserAzureAdd> GetUserAzureAD(string email)
        {
            var grapServiceClient = _graphApiServices.GetGraphApiService();

            var user = await grapServiceClient.Users[email].Request()
                                              .Expand(u => u.Manager).Select(e => new {
                                                  e.DisplayName,
                                                  e.UserPrincipalName,
                                                  e.UserType,
                                                  e.CompanyName,
                                                  e.JobTitle,
                                                  e.Department,
                                                  e.EmployeeId
                                              }).GetAsync();
            UserAzureAdd result = new UserAzureAdd
            {
                DisplayName = user.DisplayName,
                PrincipalName = user.UserPrincipalName,
                UserType = user.UserType,
                CompanyName = user.CompanyName,
                JobTitle = user.JobTitle,
                Department = user.Department,      
                EmployeeId = user.EmployeeId
            };
            return result;
        }
    }
}
