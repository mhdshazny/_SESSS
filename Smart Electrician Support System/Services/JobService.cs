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
        private static AppointmentService _appService;

        public JobService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _appService = new AppointmentService(context, mapper);
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

        public static async Task<bool> Add(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.JobEnd_Time = collection.JobAssign_Time;
                    var MapData = _mapper.Map<JobModel>(collection);
                    _context.Add(MapData);
                    _context.SaveChanges();
                    bool appStatus =  updateStatus(MapData.Appo_ID);
                    if (appStatus)
                    {
                        return true;
                    }
                    return false;
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

        private static bool updateStatus(string id)
        {
            try
            {
                AppointmentModel apptObj = _context.AppointmentData.Where(i=>i.Appo_ID==id).FirstOrDefault();
                apptObj.Appo_Status = "Accepted";
                _context.Update(apptObj);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }


        }

        internal List<JobViewModel> GetListEmpJobs(string id)
        {
            var DataList = _context.JobData.Where(i=>i.Emp_Electr_ID==id).ToList();
            var GetList = new List<JobViewModel>();
            foreach (var item in DataList)
            {
                var VM = _mapper.Map<JobViewModel>(item);
                GetList.Add(VM);
            }


            return GetList;
        }

        internal static TimeSpan DateTimeElapsed(string id)
        {
            var job = Find(id);
            TimeSpan dt = DateTime.Today - job.JobAssign_Time;

            return dt;

                
        }
        
        internal static TimeSpan DaysElapsed(string id)
        {
            var job = Find(id);
            TimeSpan dt = job.JobEnd_Time - job.JobAssign_Time;

            return dt;

                
        }

        public static async Task<bool> Update(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {

                    if (collection.JobEnd_Time == DateTime.Parse("0001 - 01 - 01 00.00.00"))
                    {
                        collection.JobEnd_Time = collection.JobAssign_Time;

                    }
                    
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
