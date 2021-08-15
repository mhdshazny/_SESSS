using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class ProductsService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public ProductsService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public static List<ProductsViewModel> GetList()
        {
            var DataList = _context.ProductsData.ToList();
            var GetList = new List<ProductsViewModel>();
            foreach (var item in DataList)
            {
                var VM = _mapper.Map<ProductsViewModel>(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public static bool Add(ProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<ProductsModel>(collection);
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
        public static async Task<bool> Update(ProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<ProductsModel>(collection);
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
            var Id = _context.ProductsData
                .Max(i => i.PrID);
            string NewID = "PRD0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "PRD" + num.ToString().PadLeft(7, '0');
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
                    ProductsModel FoundRecord = _context.ProductsData.Find(id);
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

        public static ProductsViewModel Find(string id)
        {
            var FoundRecord = _context.ProductsData.Find(id);
            var VM = _mapper.Map<ProductsViewModel>(FoundRecord);

            return VM;
        }




    }
}
