using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_Employee")]
    public class EmployeeModel
    {
        [Key]
        public string EmpID { get; set; }
        public string EmpCatID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Status { get; set; }

        //public virtual EmpCategoryModel EmpCategory { get; set; }

    }
}
