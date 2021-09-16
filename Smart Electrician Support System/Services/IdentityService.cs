using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;

namespace Smart_Electrician_Support_System.Services
{
    public class IdentityService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;
        public IdentityService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        internal bool SignUp(EmpIdentityViewModel data)
        {
            try
            {
                var ConfirmData = _mapper.Map<EmpIdentityModel>(data);
                ConfirmData.EmpStatus = "Active";
                _context.Add(ConfirmData);
                _context.SaveChanges();
                return true;
            }
            catch (Exception er)
            {
                return false;
            }




        }



        internal EmpIdentityViewModel Find(string email, string passw)
        {
            var data = _context.EmpIdentityData
                .Where(i => i.EmpEmail == email && i.EmpPassWord == passw)
                .FirstOrDefault();

            var EmpData = _context.EmployeeData.Where(x => x.EmpID == data.EmpID).FirstOrDefault();
            var EmpRole = _context.EmpCategoryData.Where(x => x.EmpCat_ID == EmpData.EmpCat_ID).FirstOrDefault();
            var VMData = _mapper.Map<EmpIdentityViewModel>(data);
            VMData.EmpRole = EmpRole.EmpCat_Type;
            return VMData;

        }
    }
}
