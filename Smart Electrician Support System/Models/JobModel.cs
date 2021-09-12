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
        public string Site_Inspected_By { get; set; }
        public DateTime Site_Inspected_Date { get; set; }
        public string Emp_Electr_ID { get; set; }
        public DateTime JobAssign_Time { get; set; }
        public DateTime JobEnd_Time { get; set; }
        public string Job_Subject { get; set; }
        public string Job_Descr { get; set; }
        public DateTime JobEnd_TimeExpected { get; set; }
        public string Job_Status { get; set; }
    }
}
