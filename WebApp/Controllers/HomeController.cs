using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.Models;
using WebApp.SecureTokenServer;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Contractor
        private IConfiguration _configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Token()
        {
            var user = User;
            string accToken = HttpContext.GetTokenAsync("access_token").Result;
            ViewData["AccessToken"] = accToken;

            return View();
        }

        public async Task<IActionResult> GetDataFromApi()
        {
            var result = string.Empty;

            var uri = _configuration["Api:Test"];
            string accToken = await HttpContext.GetTokenAsync("access_token");
            result = await ApiService.GetDataFromApi(uri, accToken);

            ViewData["Data"] = result;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
