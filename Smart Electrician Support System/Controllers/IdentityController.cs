using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class IdentityController : Controller
    {
        private DbConnectionClass _context;
        private IdentityService _identityService ;

        public IdentityController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _identityService = new IdentityService(context,mapper);
        }
        public IActionResult EmpLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EmpLogin(EmpIdentityViewModel data)
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(EmpIdentityViewModel data)
        {
            try
            {
                if (data != null)
                {
                    if (_identityService.SignUp(data) == true)
                        return RedirectToAction("EmpLogin", "Identity", "LoginSuccess");
                    else
                        return RedirectToAction("EmpLogin", "Identity", "LoginFailed");

                }
                return RedirectToAction("EmpLogin", "Identity", data);
            }
            catch (Exception er)
            {
                return RedirectToAction("EmpLogin", "Identity", data);
            }

        }

        public IActionResult Logout()
        {
            clearSession();
            return RedirectToAction("EmpLogin", "Identity", "SessionCleared");
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
                        if (SessionSet(EmpEmail,EmpPassWord))
                        {
                            return RedirectToAction("Index", "Home", "Login Success.");
                        }
                        return RedirectToAction("EmpLogin", "Identity", "SessionFailed");

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

        public bool SessionSet(string email,string passw)
        {
            try
            {
                var data = _identityService.Find(email, passw);

                HttpContext.Session.SetString("SessionEmpEmail", data.EmpEmail);
                HttpContext.Session.SetString("SessionEmpID", data.EmpID);
                HttpContext.Session.SetString("SessionEmpRole", data.EmpRole);
                HttpContext.Session.SetString("SessionEmpName", data.EmpName);

                return true;
            }
            catch (Exception err)
            {
                return false;
            }


        }

        public void clearSession()
        {
            HttpContext.Session.SetString("SessionEmpEmail", "");
            HttpContext.Session.SetString("SessionEmpID", "");
            HttpContext.Session.SetString("SessionEmpRole", "");
            HttpContext.Session.SetString("SessionEmpName", "");
        }

    }
}
