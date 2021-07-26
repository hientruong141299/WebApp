using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common.Configuration;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class GraphApiServices : IGraphApiServices
    {
        
        private readonly GetConnectADDAzure _getConnectionADDAzure;
        public GraphApiServices(IOptions<GetConnectADDAzure> getConnectADDAzure)
        {
            _getConnectionADDAzure = getConnectADDAzure.Value;
        }
        public GraphServiceClient GetGraphApiService()
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                                                                       .Create(_getConnectionADDAzure.ClientId)
                                                                       .WithTenantId(_getConnectionADDAzure.TenantId)
                                                                       .WithClientSecret(_getConnectionADDAzure.ClientSecret)
                                                                       .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);

            GraphServiceClient graphServiceClient = new GraphServiceClient(authProvider);

            return graphServiceClient;
        }
    }
}
