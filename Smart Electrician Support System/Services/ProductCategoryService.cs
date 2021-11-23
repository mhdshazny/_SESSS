using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class ProductCategoryService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public ProductCategoryService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        internal IEnumerable<ProductCategoryViewModel> GetList()
        {
            try
            {
                var dbData = _context.PrCategoryData.ToList();
                var VM = new List<ProductCategoryViewModel>();
                foreach (var item in dbData)
                {
                    var tempVM = _mapper.Map<ProductCategoryViewModel>(item);
                    VM.Add(tempVM);
                }
                return VM;
            }
            catch (Exception er)
            {
                return null;
            }
        }

        internal string NewID()
        {
            var Id = _context.PrCategoryData
                        .Max(i => i.PrdCat_ID);
            int NewID = int.Parse(Id.ToString()) + 1;
            
            return NewID.ToString();
        }

        ////
        public static bool AddData(ProductCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var VM = _mapper.Map<ProductCategoryModel>(collection);
                    _context.Add(VM);
                    _context.SaveChanges();
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

        public static async Task<bool> UpdData(ProductCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    ProductCategoryModel data = _mapper.Map<ProductCategoryModel>(collection);
                    _context.Update(data);
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

        public static ProductCategoryViewModel FindPrCat(int id)
        {
            var data = _context.PrCategoryData.Find(id);
            var VM = _mapper.Map<ProductCategoryViewModel>(data);

            return VM;
        }


        internal static async Task<bool> DelData(int id)
        {
            try
            {
                if (id >=0)
                {
                    ProductCategoryModel data = _context.PrCategoryData.Find(id);
                    _context.Remove(data);
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



        /////
    }
}
