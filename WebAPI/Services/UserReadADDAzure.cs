using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class UserReadADDAzure : IApiPermissions
    {
        public async Task<User> GetUserReadByEmai(GraphServiceClient graphServiceClient, string email)
        {
            var user = await graphServiceClient.Users[email]
                          .Request()
                          .Select(e => new
                          {
                              e.DisplayName,
                              e.UserPrincipalName,
                              e.UserType,
                              e.EmployeeId,
                              e.CompanyName,
                              e.Department,
                              e.JobTitle
                          })
                          .GetAsync();
            return user;
        }
    }
}
