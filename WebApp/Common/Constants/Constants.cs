using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Common.Constants
{
    public static class Constants
    {
        public static readonly string GetUserPath = "/api/users";
        public static readonly string GetUserADD = @"/api/users/aad/{email}";
        public static readonly string GetUserSqlById = @"/api/users/{id}";
      
    }
}
