using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class EmployeeViewModel
    {
        [DisplayName("Employee ID")]
        [Required(ErrorMessage = "Please provide a valid Employee ID.")]
        public string EmpID { get; set; }

        [DisplayName("Employee Category")]
        [Required(ErrorMessage = "Please provide a valid Employee Category.")]
        public string EmpCatID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please provide your First Name.")]
        public string fName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please provide your Last Name.")]
        public string lName { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please provide your Gender.")]
        public string Gender { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please provide a valid Date of Birth.")]
        public DateTime DoB { get; set; }

        [DisplayName("NIC No.")]
        [Required(ErrorMessage = "Please provide a valid Email address.")]
        public string NIC { get; set; }

        [DisplayName("Employee Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please provide a valid Email address.")]
        public string Email { get; set; }

        [DisplayName("Permenent Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please provide your Permenent Address.")]
        public string Address { get; set; }

        [DisplayName("Contact No.")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please provide a valid Phone Number.")]
        public string Contact { get; set; }

        [DisplayName("Employee Status")]
        [Required(ErrorMessage = "Please provide a valid Employee Status.")]
        public string Status { get; set; }

    }
}
