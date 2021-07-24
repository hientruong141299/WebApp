using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.InterfaceADDAzure
{
   public interface IUserADDAzure
    {
        Task<UserAzureAdd> GetGraphApiUser(string email);
        UserAzureAdd UserAzureAdd(Microsoft.Graph.User user);
    }
}
