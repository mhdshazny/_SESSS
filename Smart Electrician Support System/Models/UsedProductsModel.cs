using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_UsedProducts")]
    public class UsedProductsModel
    {
        [Key]
        public string Pr_Used_ID { get; set; }
        public string Job_ID { get; set; }
        public string PrID { get; set; }
        public int PrQty { get; set; }
        public string Status { get; set; }
    }
}
