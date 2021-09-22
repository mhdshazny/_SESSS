using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            var DataList = _context.JobData.Where(i=>i.Job_Status=="Accepted"||i.Job_Status=="Pending").ToList();
            var GetList = new List<JobViewModel>();
            foreach (var item in DataList)
            {                
                var VM = _mapper.Map<JobViewModel>(item);
                GetList.Add(VM);
            }


            return GetList;
        }
        public static List<JobViewModel> GetListAll()
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

        public static async Task<bool> AddAsync(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.JobEnd_Time = collection.JobAssign_Time;
                    var MapData = _mapper.Map<JobModel>(collection);
                    await _context.AddAsync(MapData);
                    await _context.SaveChangesAsync();
                    bool appStatus = await  updateStatusAsync(MapData.Appo_ID, "Accepted");
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
                return false;
            }
        }

        internal static List<JobViewModel> GetListForDD()
        {
            var data = GetList();

            try
            {
                foreach (var item in data)
                {
                    item.Job_Subject = "[" + item.Job_ID + "] " + item.Job_Subject;
                }
                return data;
            }
            catch (Exception er)
            {
                return data;
            }

        }

        internal static List<JobViewModel> GetListForElectrician(string id)
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

        private static async Task<bool> updateStatusAsync(string id,string status)
        {
            try
            {
                AppointmentModel apptObj = await _context.AppointmentData.Where(i=>i.Appo_ID==id).FirstOrDefaultAsync();
                apptObj.Appo_Status = status;
                _context.Update(apptObj);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }


        }

        internal async Task<bool> FinishJobAsync(string job_ID, DateTime jobEnd_Time)
        {
            try
            {
                if (job_ID.Length>0)
                {
                    var collection = await _context.JobData.FindAsync(job_ID);
                    var MapData = _mapper.Map<JobModel>(collection);
                    MapData.JobEnd_Time = jobEnd_Time;
                    MapData.Job_Status = "Finished";
                    _context.Update(MapData);
                    await _context.SaveChangesAsync();
                    bool appStatus = await updateStatusAsync(MapData.Appo_ID, "Finished");
                    bool InvoiceReady = await InvoiceReadyAsync(MapData);
                    if (appStatus==true)
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
                return false;
            }
        }

        private async Task<bool> InvoiceReadyAsync(JobModel jobData)
        {
            try
            {
                InvoiceModel obj = new InvoiceModel();

                obj.Job_ID = jobData.Job_ID;
                obj.Inv_Date = jobData.JobEnd_Time;
                obj.Inv_GenBy = jobData.Emp_Electr_ID;
                obj.Inv_PaymentStatus = "Complete";
                obj.Inv_Status = "Ready";
                obj.Inv_TotAmnt = await invoiceAmountAsync(jobData.Job_ID);
                obj.Inv_ID = NewInvID();


                _context.Add(obj);
                await _context.SaveChangesAsync();


                return true;
            }
            catch (Exception er)
            {
                return false;
            }


        }

        private async Task<decimal> invoiceAmountAsync(string id)
        {
            decimal tot = 0;

            var UsedPrddata =await _context.UsedProductsData.Where(i => i.Job_ID == id).ToListAsync();
            var Prddata =await _context.ProductsData.ToListAsync();

            foreach (var item in UsedPrddata)
            {
                var prd = Prddata.Where(i=>i.PrID==item.PrID).FirstOrDefault();
                var tempTot = prd.PrPrice * item.PrQty;

                tot = tot + tempTot;
            }

            return tot;


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

        internal static string NewInvID()
        {
            var Id = _context.InvoiceData
                .Max(i => i.Inv_ID);
            string NewID = "INV0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "INV" + num.ToString().PadLeft(7, '0');
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
