using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class EmployeeJobsViewModel
    {
        [DisplayName("Emp ID")]
        [Required(ErrorMessage = "Please provide a valid Employee ID.")]
        public string EmpID { get; set; }

        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "Please provide your First Name.")]
        public string Name { get; set; }

        [DisplayName("Assigned Jobs Count")]
        public int JobCount { get; set; }

        [DisplayName("Jobs Done Count")]
        public int JobsDoneCount { get; set; }

        [DisplayName("Jobs Done On Time")]
        public int JobsDOTCount { get; set; }

        [DisplayName("Jobs Pending Count")]
        public int JobsPendingCount { get; set; }

        [DisplayName("Average Jobs Per Month")]
        public int AvgJobsPerMonths { get; set; }

        [DisplayName("Jobs Cancelled")]
        public int JobsCancelledCount { get; set; }

        [DisplayName("Employee Performance Status")]
        public string PerfStatus { get; set; }

    }
}
