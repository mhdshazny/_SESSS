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
        public string Job_ID { get; set; }
        [DisplayName("Appointment ID")]
        public string Appo_ID { get; set; }
        [DisplayName("Site Inspecter")]
        public string Site_Inspected_By { get; set; }
        [DisplayName("Site Inspected Date")]
        [DataType(DataType.Date)]
        public DateTime Site_Inspected_Date { get; set; }
        [DisplayName("Electrician ID")]
        public string Emp_Electr_ID { get; set; }
        [DisplayName("Job Assigned Time")]
        [DataType(DataType.DateTime)]
        public DateTime JobAssign_Time { get; set; }
        [DisplayName("Job End ID")]
        [DataType(DataType.DateTime)]
        public DateTime JobEnd_Time { get; set; }
        [DisplayName("Job Subject")]
        public string Job_Subject { get; set; }
        [DisplayName("Job Description")]
        public string Job_Descr { get; set; }
        [DisplayName("Requested Components")]
        public string Req_Components { get; set; }
        [DisplayName("Job Status")]
        public string Job_Status { get; set; }
    }
}
