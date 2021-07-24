using Microsoft.AspNetCore.Mvc;

using Microsoft.Graph;
using System.Diagnostics;
using System.Threading.Tasks;

using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly  IUserLogic _userLogic;
      
        public HomeController(IUserLogic userLogic)
        {   
            _userLogic = userLogic;
        }
        public async Task<IActionResult>Index()
        {
            var response = await _userLogic.GetAllUsers();
           
            return View(response);
        }
        [HttpGet]
        public async Task<JsonResult> GetDetail(int id)
        {
            var employee = await _userLogic.GetUserSqlById(id);
            return Json(new
            {
                data = employee,
                status = true
            });
        }
        [HttpGet]
        public async Task<JsonResult> GetDetailUserADD(string email)
        {
            var employee = await _userLogic.GetUserAddByEmail(email);
            return Json(new
            {
                data = employee,
                status = true
            });
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
