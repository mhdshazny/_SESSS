using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Models;

namespace Smart_Electrician_Support_System.Services
{
    public class IdentityService
    {
        private static DbConnectionClass _context;
        private List<EmpIdentityModel> EmpList;

        public IdentityService(DbConnectionClass context)
        {
            _context = context;
        }

        public bool VerifyEmail(string EmpEmail)
        {
            if (EmpEmail != null)
            {
                bool state = EmpListTemp(EmpEmail);
                if (state == true)
                    return true;
                else
                    return false;
            }

            else
                return false;
        }

        public bool EmpListTemp(string EmpEmail)
        {
            //IEnumerable<EmpIdentityModel> TempModel;
            EmpList = _context.EmpIdentityData.ToList();
            if (EmpList.Count > 0)
                return true;
            else
                return false;
            
        } 


        public static bool VerifyLogin(string EmpEmail, string EmpPassWord)
        {
            var Confirm = _context.EmpIdentityData.Where(i => i.EmpEmail==EmpEmail && i.EmpPassWord==EmpPassWord).ToList(); 
            if (EmpEmail == "Hello@hello.com")
                return true;
            else
                return false;
        }
    }
}
