using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class CustomerViewModel
    {
        [DisplayName("Customer ID")]
        [Required(ErrorMessage = "Please provide a valid Customer ID.")]
        public string CusID { get; set; }
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Please provide a valid Customer First Name.")]
        public string CusfName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please provide a valid Customer Last Name.")]
        public string CuslName { get; set; }
        [DisplayName("Customer NIC")]
        [Required(ErrorMessage = "Please provide a valid Customer NIC.")]
        public string CusNIC { get; set; }
        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please provide a valid Customer Gender.")]
        public string CusGender { get; set; }
        [DisplayName("Permenent Address")]
        [Required(ErrorMessage = "Please provide a valid Customer Address.")]
        [DataType(DataType.MultilineText)]
        public string CusAddress { get; set; }
        [DisplayName("Contact No.")]
        [Required(ErrorMessage = "Please provide a valid Customer Mobile.")]
        public string CusContact { get; set; }
        [DisplayName("Email ID")]
        [Required(ErrorMessage = "Please provide a valid Customer Email.")]
        [DataType(DataType.EmailAddress)]
        public string CusEmail { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide a valid Customer Password.")]
        public string CusPassw { get; set; }
        [DisplayName("Customer Company Name")]
        [Required(ErrorMessage = "Please provide a valid Customer Company Name.")]
        public string CusProperty { get; set; }
        [DisplayName("Customer Status")]
        [Required(ErrorMessage = "Please provide a valid Customer Status.")]
        public string CusStatus { get; set; }
    }
}
