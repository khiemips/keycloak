using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.SecureTokenServer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel entry)
        {
            if (ModelState.IsValid)
            {
                var result = await KeyCloak.GenarateToken(entry);
                return Ok();
            }

            return View(entry);
        }
    }
}