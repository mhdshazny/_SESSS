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
        [DisplayName("Login ID")]
        public string LoginID { get; set; }

        [DisplayName("Employee ID")]
        public string EmpID { get; set; }
                      
        [DisplayName("Employee Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Please provide a valid Email address.")]
        public string EmpEmail { get; set; }
                      
        [DisplayName("Employee Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Please enter a valid Password.")]
        public string EmpPassWord { get; set; }
                      
        [DisplayName("Employee Status")]
        public string EmpStatus { get; set; }
    }
}
