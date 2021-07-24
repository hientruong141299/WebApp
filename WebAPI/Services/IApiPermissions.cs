using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
   public interface IApiPermissions
    {
        Task<Microsoft.Graph.User> GetUserReadByEmai(GraphServiceClient graphServiceClient,string email);
    }
}
