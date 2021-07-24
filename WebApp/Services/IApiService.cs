using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebApp.Services
{
   public interface IApiService
    {
        Task<string> GetApi(string url);
        Task<string> GetApiUserAdd(string url);
    }
}
