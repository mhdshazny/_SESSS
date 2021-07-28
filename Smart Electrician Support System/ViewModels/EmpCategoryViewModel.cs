using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class EmpCategoryViewModel
    {
        [DisplayName("Employee Category ID")]
        [Required(ErrorMessage = "Please provide a valid ID.")]
        public string EmpCat_ID { get; set; }
        [DisplayName("Employee Type")]
        [Required(ErrorMessage = "Please provide a valid Category")]
        public string EmpCat_Type { get; set; }
        [DisplayName("Category Description")]
        [DataType(DataType.MultilineText)]
        public string EmpCat_Descr { get; set; }
        [DisplayName("Category Status")]
        [Required(ErrorMessage = "Please provide the category status.")]
        public string EmpCat_Status { get; set; }
    }
}
