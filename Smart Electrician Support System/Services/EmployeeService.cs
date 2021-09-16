﻿using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class EmployeeService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public EmployeeService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public static List<EmployeeViewModel> GetList()
        {
            var DataList = _context.EmployeeData.ToList();
            var GetList = new List<EmployeeViewModel>();
            foreach (var item in DataList)
            {
                var EmpCatVM = _mapper.Map<EmployeeViewModel>(item);
                EmpCatVM.EmpCat_ID = EmpCat(EmpCatVM.EmpCat_ID);
                GetList.Add(EmpCatVM);
            }
            return GetList;
        }

        internal static List<EmployeeViewModel> GetListByType(string type)
        {
            var empType = _context.EmpCategoryData.Where(i => i.EmpCat_Type == type).FirstOrDefault();

            var DataList = _context.EmployeeData.Where(i=>i.EmpCat_ID==empType.EmpCat_ID).ToList();
            var GetList = new List<EmployeeViewModel>();
            foreach (var item in DataList)
            {
                var EmpCatVM = _mapper.Map<EmployeeViewModel>(item);
                EmpCatVM.EmpCat_ID = EmpCat(EmpCatVM.EmpCat_ID);
                GetList.Add(EmpCatVM);
            }
            return GetList;
        }

        internal static bool ChangePass(string email, string oldPassw, string newPassw)
        {
            bool state = false;
            var data = _context.EmpIdentityData.Where(i=>i.EmpEmail==email&&i.EmpPassWord==oldPassw).FirstOrDefault();

            if (data!=null)
            {
                data.EmpPassWord = newPassw;
                _context.Update(data);
                _context.SaveChanges();
                state = true;
            }

            return state;
            
        }

        public static string EmpCat(string id)
        {
            var CatName = _context.EmpCategoryData.Find(id);
            return CatName.EmpCat_Type;
        }

        public static bool Add(EmployeeViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<EmployeeModel>(collection);
                    _context.Add(MapData);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }


        public static async Task<bool> Update(EmployeeViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<EmployeeModel>(collection);
                    _context.Update(MapData);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }
        internal static string NewID()
        {
            var Id = _context.EmployeeData
                .Max(i => i.EmpID);
            string NewID = "EMP0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "EMP" + num.ToString().PadLeft(7, '0');
                return NewID;
            }
            return NewID;
        }

        internal static async Task<bool> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    EmployeeModel FoundRecord = _context.EmployeeData.Find(id);
                    _context.Remove(FoundRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static EmployeeViewModel Find(string id)
        {
            var FoundRecord = _context.EmployeeData.Find(id);
            var VM = _mapper.Map<EmployeeViewModel>(FoundRecord);

            return VM;
        }
    }
}
