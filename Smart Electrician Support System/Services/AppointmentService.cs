using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class AppointmentService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public AppointmentService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public static List<AppointmentViewModel> GetList()
        {
            var DataList = _context.AppointmentData.ToList();
            var GetList = new List<AppointmentViewModel>();
            foreach (var item in DataList)
            {
                var VM = _mapper.Map<AppointmentViewModel>(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public static bool Add(AppointmentViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<AppointmentModel>(collection);
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
        public static async Task<bool> Update(AppointmentViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<AppointmentModel>(collection);
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
            var Id = _context.AppointmentData
                .Max(i => i.Appo_ID);
            string NewID = "APP0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "APP" + num.ToString().PadLeft(7, '0');
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
                    AppointmentModel FoundRecord = _context.AppointmentData.Find(id);
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

        public static AppointmentViewModel Find(string id)
        {
            var FoundRecord = _context.AppointmentData.Find(id);
            var VM = _mapper.Map<AppointmentViewModel>(FoundRecord);

            return VM;
        }




    }
}
