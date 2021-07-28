using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class IdentityController : Controller
    {
        public DbConnectionClass _context { get; set; }
        public IdentityService _identityService ;

        public IdentityController(DbConnectionClass context)
        {
            _context = context;
            _identityService = new IdentityService(context);
        }
        public IActionResult EmpLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string EmpEmail, string EmpPassWord)
        {
            try
            {

                if (ModelState.IsValid == false)
                {

                    return RedirectToAction("EmpLogin", "Identity", "ModelState Invalid");
                }
                if (EmpEmail == null && EmpPassWord == null)
                {
                    return RedirectToAction("EmpLogin", "Identity", "Null Values");
                }
            
                bool EmailValid = IdentityService.VerifyEmail(EmpEmail);

                if (EmailValid==true)
                {
                    if (IdentityService.VerifyLogin(EmpEmail,EmpPassWord) == true)
                    {
                        return RedirectToAction("Home", "EmpDashboard", "Login Success.");
                    }
                    else
                    {
                        return RedirectToAction("EmpLogin", "Identity", "Login credentials didn't match.");
                    }
                }
                else
                {
                    return RedirectToAction( "Login","Identity","Email Not Found.");
                }        
            }

            catch(Exception err)
            {
                return RedirectToAction("EmpLogin", "Identity", err.Message);

            }
        }

    }
}
