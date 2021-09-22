using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class UsedProductsViewModel
    {
        [DisplayName("Used Product ID")]
        [Required(ErrorMessage = "Please provide a valid ID.")]
        public string Pr_Used_ID { get; set; }
        [DisplayName("Job ID")]
        [Required(ErrorMessage = "Please provide a valid/relevant Job ID.")]
        public string Job_ID { get; set; }
        [DisplayName("Product ID")]
        [Required(ErrorMessage = "Please provide a valid Product ID.")]
        public string PrID { get; set; }
        [DisplayName("Product Quantity")]
        [Required(ErrorMessage = "Please provide a valid Product Quantity.")]
        public int PrQty { get; set; }
        [DisplayName("Used Product Status")]
        [Required(ErrorMessage = "Please provide a valid Product Status.")]
        public string Status { get; set; }
        [NotMapped]
        [DisplayName("Product Name")]
        public string PrName { get; set; }
        [NotMapped]
        [DisplayName("Total Cost")]
        [DataType(DataType.Currency)]
        public string TotCost { get; set; }
    }
}
