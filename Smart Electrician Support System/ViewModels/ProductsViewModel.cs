using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class ProductsViewModel
    {
        [DisplayName("Product ID")]
        [Required(ErrorMessage = "Please provide a valid Product ID.")]
        public string PrID { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please provide a valid Product Name.")]
        public string PrName { get; set; }
        [DisplayName("Product Description")]
        [Required(ErrorMessage = "Please provide a valid Product Description.")]
        public string PrDescr { get; set; }
        [DisplayName("Product Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please provide a valid Product Price.")]
        public Decimal PrPrice { get; set; }
        [DisplayName("Product Status")]
        [Required(ErrorMessage = "Please provide a valid Product Status.")]
        public string PrStatus { get; set; }
    }
}
