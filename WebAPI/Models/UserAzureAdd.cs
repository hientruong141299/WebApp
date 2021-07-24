using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserAzureAdd
    {
        public string DisplayName { get; set; }
        public string PrincipalName { get; set; }
        public string UserType { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string EmployeeId { get; set; }
        public string Department { get; set; }

    }
}
