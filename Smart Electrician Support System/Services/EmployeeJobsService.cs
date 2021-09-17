using AutoMapper;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class EmployeeJobsService
    {
        private static DbConnectionClass _context;
        private static IMapper _mapper;
        private static EmployeeService _empService;
        private static JobService _jobService;

        public EmployeeJobsService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _empService = new EmployeeService(context, mapper);
            _jobService = new JobService(context, mapper);
        }

        public List<EmployeeViewModel> GetNameList()
        {
            List<EmployeeViewModel> obj = EmployeeService.GetList();

            return obj;
        }

        internal EmployeeJobsViewModel GetEmpPerf(string id)
        {
            EmployeeJobsViewModel obj = new EmployeeJobsViewModel();

            EmployeeViewModel emp = EmployeeService.Find(id);
            List<JobViewModel> jobs = _jobService.GetListEmpJobs(id);

            List<int> monthLi = new List<int>();
            foreach (var item in jobs)
            {
                int monthCount = item.JobEnd_Time.Month;
                monthLi.Add(monthCount);
            }

            if (jobs.Count > 0)
            {
                monthLi.Distinct();
                obj.AvgJobsPerMonths = jobs.Count / monthLi.Count;

                obj.EmpID = emp.EmpID;
                obj.Name = emp.fName + " " + emp.lName;
                obj.JobCount = jobs.Count;
                obj.JobsDoneCount = jobs.Where(i => i.Job_Status == "Finished").Count();
                obj.JobsCancelledCount = jobs.Where(i => i.Job_Status == "Cancelled").Count();
                obj.JobsDOTCount = jobs.Where(i => i.Job_Status == "Completed_DOT").Count();
                obj.JobsPendingCount = jobs.Where(i => i.Job_Status == "Accepted").Count();

                if (obj.JobCount == obj.JobsDOTCount)
                {
                    obj.PerfStatus = "OutStanding Performance";
                }
                if (obj.JobCount == obj.JobsDoneCount)
                {
                    obj.PerfStatus = "Excellent Performance";
                }
                int JobsPending = jobs.Where(i => i.Job_Status == "Pending").Count();
                if (obj.JobsDoneCount < JobsPending)
                {
                    obj.PerfStatus = "Poor Performance";
                }
            }
            else
            {
                obj.Name = emp.fName + " " + emp.lName;
                obj.PerfStatus = "No Data Found About Regarding :" +id;
            }

            return obj;



        }

        internal int[] GraphData(string id)
        {
            //int[] li = new int[] { 100, 70, 50, 150, 100, 200, 250, 200, 250, 300, 350, 400 };
            int[] li = new int[12];

            List<JobViewModel> jobs = _jobService.GetListEmpJobs(id);

            for (int i =0; i < 12; i++)
            {
                int count = jobs.Where(x=>x.JobEnd_Time.Month==i+1).Count();
                li[i] = count;
            }



            return li;
        }
    }
}
