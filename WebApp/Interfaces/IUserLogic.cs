using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebApp.Interfaces
{
    public interface IUserLogic
    {       
        Task<List<User>> GetAllUsers();
        Task<User> GetUserSqlById(int id);
        Task<UserAzureAdd> GetUserAddByEmail(string email);
        
    }
}
