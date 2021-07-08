using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("tbl_EmpData")]
    public class EmpIdentityModel
    {
        [Key]
        public string EmpID { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPassWord { get; set; }
        public string EmpStatus { get; set; }
    }
}
