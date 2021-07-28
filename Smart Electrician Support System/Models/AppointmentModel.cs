using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_Appointment")]
    public class AppointmentModel
    {
        [Key]
        public string Appo_ID { get; set; }
        public string Cus_ID { get; set; }
        public string Appo_Subject { get; set; }
        public DateTime Appo_Time { get; set; }
        public string Appo_Descr { get; set; }
        public string Appo_HandledBy { get; set; }
        public string Appo_Status { get; set; }

        //public virtual EmployeeModel Employee { get; set; }
        //public virtual CustomerModel Customer { get; set; }

    }
}
