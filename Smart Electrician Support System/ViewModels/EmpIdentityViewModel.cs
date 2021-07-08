using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class EmpIdentityViewModel
    {
        [DisplayName("Employee ID")]
        public string EmpID { get; set; }
                      
        [DisplayName("Employee Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Please provide a valid Email address.")]
        public string EmpEmail { get; set; }
                      
        [DisplayName("Employee Password")]
        [Required]
        public string EmpPassWord { get; set; }
                      
        [DisplayName("Employee Status")]
        public string EmpStatus { get; set; }
    }
}
