using Smart_Electrician_Support_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class JobViewModel
    {

        [DisplayName("Job ID")]
        [Required(ErrorMessage ="Job ID must be filled.")]
        public string Job_ID { get; set; }
        [DisplayName("Appointment ID")]
        [Required(ErrorMessage = "Please select a valid Appointment Detatils")]
        public string Appo_ID { get; set; }
        [DisplayName("Site Inspecter")]
        [Required(ErrorMessage = "Site must be indspected before Assigning the JOB")]
        public string Site_Inspected_By { get; set; }
        [DisplayName("Site Inspected Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Site Inspected Date")]
        public DateTime Site_Inspected_Date { get; set; }
        [DisplayName("Electrician ID")]
        [Required(ErrorMessage = "Select the Electrician for this Job")]
        public string Emp_Electr_ID { get; set; }
        [DisplayName("Job Assigned Time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Job Assigning DateTime. (Better to Assign within the Day)")]
        public DateTime JobAssign_Time { get; set; }
        [DisplayName("Job End DateTime (Expected)")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Expected DateTime to Finish the JOB. (Set According to Urgency)")]
        public DateTime JobEnd_Time { get; set; }
        [DisplayName("Job Subject")]
        [Required(ErrorMessage = "Suitable Subject Needed to proceed")]
        public string Job_Subject { get; set; }
        [DisplayName("Job Description")]
        public string Job_Descr { get; set; }
        [DisplayName("Expected Job Completion On")]
        public DateTime JobEnd_TimeExpected { get; set; }
        [DisplayName("Job Status")]
        public string Job_Status { get; set; }
    }
}
