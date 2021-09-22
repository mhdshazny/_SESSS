using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class DashboardService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;

        public DashboardService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DashTargetViewModel Get()
        {
            var obj = _context.DashTargetData.Where(i => i.status == "Active").FirstOrDefault();
            var objVM = _mapper.Map<DashTargetViewModel>(obj);

            objVM.AvgMonth = calcTotJobs();
            objVM.TargetAchieved = (float.Parse(objVM.AvgMonth.ToString()) / float.Parse(obj.monthTarget.ToString())) * 100;
            objVM.PendingJobs = calcPendingJobs();

            return objVM;
        }

        private int calcPendingJobs()
        {
            var data = _context.JobData.Where(i => i.Job_Status == "Pending").Count();

            return data;
        }

        private float calcTotJobs()
        {
            var data = _context.JobData.ToList();
            float count = float.Parse(data.Count.ToString());

            return count;
        }

        public bool Update(DashTargetViewModel obj)
        {
            try
            {
                if (obj != null)
                {
                    var MapData = _mapper.Map<DashTargetModel>(obj);
                    _context.Update(MapData);
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

        internal int[] GetJobStatus()
        {
            int[] outData = new int[5];

            var data = _context.JobData.ToList();

            outData[0] = data.Where(i => i.Job_Status == "Finished").Count();
            outData[1] = data.Where(i => i.Job_Status == "Pending").Count();
            outData[2] = data.Where(i => i.Job_Status == "Accepted").Count();
            outData[3] = data.Where(i => i.Job_Status == "Closed").Count();
            outData[4] = data.Where(i => i.Job_Status == "Terminated").Count();

            return outData;
        }

        internal List<int> GetJobsMonthData()
        {
            var data = _context.JobData.ToList();
            List<int> outData = new List<int>();
            for (int i = 0; i < 12; i++)
            {
                int count = 0;
                foreach (var item in data)
                {
                    if (item.JobEnd_Time.Month==i)
                    {
                        count = count + 1;
                    }
                }
                outData.Add(count);
            }
            return outData;
        }

        internal DashboardTopEmployeesViewModel[] GetTopEmps()
        {
            List<DashboardTopEmployeesViewModel> li = new List<DashboardTopEmployeesViewModel>();
            DashboardTopEmployeesViewModel[] final = new DashboardTopEmployeesViewModel[4];

            var JobList = _context.JobData.ToList();
            var EmpData = JobList.Select(x=>x.Emp_Electr_ID).ToList().Distinct();
            //var empLi = _context.EmployeeData.Where(i=>i.EmpCat_ID== "EMPCAT0004").ToList();

    

            foreach (var EmpItem in EmpData)
            {
                var empInfo = _context.EmployeeData.Find(EmpItem);

                DashboardTopEmployeesViewModel obj = new DashboardTopEmployeesViewModel();
                obj.EmpID = EmpItem;
                obj.EmpName = empInfo.fName + " " + empInfo.lName;
                obj.num = 0;

                double jobCount = 0;
                double DoneCount = 0;
                
                foreach (var JobItem in JobList)
                {
                    if (JobItem.Emp_Electr_ID==EmpItem)
                    {
                        if (JobItem.Job_Status == "Finished")
                        {
                            DoneCount++;
                        }
                        jobCount++;
                    }

                }
                double score = (DoneCount / jobCount) * 100;
                obj.score = score;
                li.Add(obj);

                //final = li.OrderByDescending(i => i.score).ToArray();

                if (li.Count() > 4)
                {
                    break;
                }
            }





            final = li.OrderByDescending(i => i.score).ToArray();


            return final;

        }
    }
}
