using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class JobService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public JobService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public static List<JobViewModel> GetList()
        {
            var DataList = _context.JobData.ToList();
            var GetList = new List<JobViewModel>();
            foreach (var item in DataList)
            {
                var VM = _mapper.Map<JobViewModel>(item);
                GetList.Add(VM);
            }
            return GetList;
        }

        public static bool Add(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<JobModel>(collection);
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
        public static async Task<bool> Update(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var MapData = _mapper.Map<JobModel>(collection);
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
            var Id = _context.JobData
                .Max(i => i.Job_ID);
            string NewID = "JOB0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "JOB" + num.ToString().PadLeft(7, '0');
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
                    JobModel FoundRecord = _context.JobData.Find(id);
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

        public static JobViewModel Find(string id)
        {
            var FoundRecord = _context.JobData.Find(id);
            var VM = _mapper.Map<JobViewModel>(FoundRecord);

            return VM;
        }

        //
        internal static IEnumerable JobList()
        {
            var EmpCat = _context.JobData.ToList();
            var empCatV = new List<JobViewModel>();
            foreach (var item in EmpCat)
            {
                var EmpCatVM = _mapper.Map<JobViewModel>(item);
                empCatV.Add(EmpCatVM);
            }
            return empCatV;        
        }
    }
}
