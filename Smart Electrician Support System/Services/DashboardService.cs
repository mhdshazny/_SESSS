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

            return objVM;
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

        internal DashboardTopEmployeesViewModel[] GetTopEmps()
        {
            List<DashboardTopEmployeesViewModel> li = new List<DashboardTopEmployeesViewModel>();
            DashboardTopEmployeesViewModel[] final = new DashboardTopEmployeesViewModel[5];

            var data = _context.JobData.ToList();
            var empLi = _context.EmployeeData.Where(i=>i.EmpCat_ID== "EMPCAT0004").ToList();


            foreach(var item in empLi)
            {
                var details = data.Where(i => i.Emp_Electr_ID == item.EmpID).ToList();
                int totJobs = details.Count();
                int doneJobs = details.Where(i => i.Job_Status == "Finished").Count();

                DashboardTopEmployeesViewModel temp = new DashboardTopEmployeesViewModel();
                temp.EmpID = item.EmpID;
                temp.EmpName = item.fName + " " + item.lName;
                temp.num = 0;
                double score = (doneJobs / totJobs) * 100;
                temp.score = score;

                li.Add(temp);
            }

            final= li.OrderByDescending(i=>i.score).ToArray();


            return final;

        }
    }
}
