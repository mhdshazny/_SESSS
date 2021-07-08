using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //string Email = "Hello1@hello.com";
            //int val=0;
            //Boolean EmailVerify = IdentityService.VerifyEmail(Email);
            //if (EmailVerify == true)
            //    val = val + 1;
            //else
            //    val = val + 2;
            return View();
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
