using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_Job")]
    public class JobModel
    {
        [Key]
        public string Job_ID { get; set; }
        public string Appo_ID { get; set; }
        public string Site_Inspected_ID { get; set; }
        public string Emp_Elctr_ID { get; set; }
        public DateTime Job_Assign_Time { get; set; }
        public DateTime Job_End_Time { get; set; }
        public string Job_Subject { get; set; }
        public string Job_Descr { get; set; }
        public string Req_Components { get; set; }
        public string Job_Status { get; set; }
    }
}
