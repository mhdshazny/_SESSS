using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_Products")]
    public class ProductsModel
    {
        [Key]
        public string PrID { get; set; }
        public string PrName { get; set; }
        public string PrDescr { get; set; }
        public Decimal PrPrice { get; set; }
        public string PrStatus { get; set; }

    }
}
