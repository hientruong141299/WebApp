using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common.Configuration;
using WebAPI.InterfaceADDAzure;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.ADDAzure
{
    public class GraphApiClientDirect:IUserADDAzure
    {
        private readonly GetConnectADDAzure _getConnectionADDAzure;
        private readonly IApiPermissions _apiPermissions;
        
       
        public GraphApiClientDirect(IOptions<GetConnectADDAzure> getConnectADDAzure,IApiPermissions apiPermissions)
        {
            _getConnectionADDAzure=getConnectADDAzure.Value;
            _apiPermissions = apiPermissions;
           
        }

        public async Task<UserAzureAdd> GetGraphApiUser(string email)
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(_getConnectionADDAzure.ClientId)
            .WithTenantId(_getConnectionADDAzure.TenantId)
            .WithClientSecret(_getConnectionADDAzure.ClientSecret)
            .Build();
            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphServiceClient = new GraphServiceClient(authProvider);
            var users = await _apiPermissions.GetUserReadByEmai(graphServiceClient, email);
            var results = UserAzureAdd(users);         
            return results;
        }

        public UserAzureAdd UserAzureAdd(Microsoft.Graph.User users)
        {
            UserAzureAdd result = new UserAzureAdd()
            {
                DisplayName = users.DisplayName,
                PrincipalName = users.UserPrincipalName,
                UserType = users.UserType,
                CompanyName = users.CompanyName,
                EmployeeId = users.EmployeeId,
                JobTitle = users.JobTitle,
                Department = users.Department,
            };
            return result;
        }

       
    }
}


