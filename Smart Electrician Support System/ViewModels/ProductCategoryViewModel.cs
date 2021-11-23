using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class ProductCategoryViewModel
    {
        [DisplayName("Category ID")]
        [Required]
        public int PrdCat_ID { get; set; }
        [DisplayName("Category Type")]
        [Required]
        public string PrdCat_Type { get; set; }
        [DisplayName("Description")]
        public string PrdCat_Description { get; set; }
        [DisplayName("Status")]
        [Required]
        public string PrdCat_Status { get; set; }
    }
}
