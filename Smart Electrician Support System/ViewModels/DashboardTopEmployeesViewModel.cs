using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class DashboardTopEmployeesViewModel : IComparable<DashboardTopEmployeesViewModel>
    {
        public int num { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public double score { get; set; }

        public int CompareTo(DashboardTopEmployeesViewModel obj)
        {
            if (obj.score < this.score)
                return 1;
            else if (obj.score > this.score)
                return 0;
            else
                return -1;
        }
    }
}
