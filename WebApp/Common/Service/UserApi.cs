using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Common.Service
{
    public class UserApi
    {
        public string GetPathUser()
        {
            var path = "api/users";
            return path;
        }
    }
}
