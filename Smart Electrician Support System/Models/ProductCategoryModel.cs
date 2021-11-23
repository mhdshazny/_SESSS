using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_ProductCategory")]
    public class ProductCategoryModel
    {
        [Key]
        public int PrdCat_ID { get; set; }
        public string PrdCat_Type { get; set; }
        public string PrdCat_Description { get; set; }
        public string PrdCat_Status { get; set; }
    }
}
