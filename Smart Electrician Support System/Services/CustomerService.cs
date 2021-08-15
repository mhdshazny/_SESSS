using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Smart_Electrician_Support_System.Services
{
    public class CustomerService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public CustomerService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public static List<CustomerViewModel> GetList()
        {
            var DataList = _context.CustomerData.ToList();
            var GetList = new List<CustomerViewModel>();
            foreach (var item in DataList)
            {
                var VM = _mapper.Map<CustomerViewModel>(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public static bool Add(CustomerViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<CustomerModel>(collection);
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

        public static async Task<bool> Update(CustomerViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<CustomerModel>(collection);
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
            var Id = _context.CustomerData
                .Max(i => i.CusID);
            string NewID = "CUS0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "CUS" + num.ToString().PadLeft(7, '0');
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
                    CustomerModel FoundRecord = _context.CustomerData.Find(id);
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

        public static CustomerViewModel Find(string id)
        {
            var FoundRecord = _context.CustomerData.Find(id);
            var VM = _mapper.Map<CustomerViewModel>(FoundRecord);

            return VM;
        }


    }
}
