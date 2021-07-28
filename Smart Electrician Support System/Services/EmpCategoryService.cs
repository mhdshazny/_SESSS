using AutoMapper;
using Microsoft.AspNetCore.Http;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class EmpCategoryService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public EmpCategoryService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public static List<EmpCategoryViewModel> EmpCatList()
        {
            var EmpCat = _context.EmpCategoryData.ToList();
            var empCatV = new List<EmpCategoryViewModel>();
            foreach (var item in EmpCat)
            {
                var EmpCatVM = _mapper.Map<EmpCategoryViewModel>(item);
                empCatV.Add(EmpCatVM);
            }
            return empCatV;
        }

        public static bool AddData(EmpCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var EmpCatVM = _mapper.Map<EmpCategoryModel>(collection);
                    EmpCatVM.EmpCat_Status = "Active";
                    _context.Add(EmpCatVM);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                
                return false;
            }
        }

        public static async Task<bool> UpdData(EmpCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    EmpCategoryModel EmpCatVM = _mapper.Map<EmpCategoryModel>(collection);
                    EmpCatVM.EmpCat_Status = "Active";
                    _context.Update(EmpCatVM);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                
                return false;
            }
        }

        public static EmpCategoryViewModel FindEmpCat(string id)
        {
            var EmpCat = _context.EmpCategoryData.Find(id);
            var empCatV = new EmpCategoryViewModel();
            var EmpCatVM = _mapper.Map<EmpCategoryViewModel>(EmpCat);
           
            return EmpCatVM;
        }

        internal static bool AvailCat(string id)
        {
            var EmpCat = _context.EmpCategoryData.Find(id);
            if (EmpCat == null)
            {
                return true;
            }
            return true;
        }

        internal static async Task<bool> DelData(string id)
        {
            try
            {
                if (id != null)
                {
                    EmpCategoryModel EmpCatVM = _context.EmpCategoryData.Find(id);
                    _context.Remove(EmpCatVM);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {

                return false;
            }
        }
    }
}
