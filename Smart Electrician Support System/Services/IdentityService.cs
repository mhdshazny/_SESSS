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

        public IdentityService(DbConnectionClass context)
        {
            _context = context;
        }

        public static bool VerifyEmail(string EmpEmail)
        {
            if (EmpEmail != null)
            {
                var EmpList = _context.EmpIdentityData.Where(i => i.EmpEmail == EmpEmail).ToList();

                if (EmpList != null)
                    return true;
                else
                    return false;
            }

            else
                return false;
        }

        public static bool VerifyLogin(string EmpEmail, string EmpPassWord)
        {
            var ConfirmData = _context.EmpIdentityData.Where(i => i.EmpEmail == EmpEmail && i.EmpPassWord == EmpPassWord).ToList();
            if (ConfirmData.Count > 0)
                return true;
            else
                return false;
        }
    }
}
