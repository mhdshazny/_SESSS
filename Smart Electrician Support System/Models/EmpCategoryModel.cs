using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_EmpCategory")]
    public class EmpCategoryModel
    {
        [Key]
        public string EmpCat_ID { get; set; }
        public string EmpCat_Type { get; set; }
        public string EmpCat_Descr { get; set; }
        public string EmpCat_Status { get; set; }

        //public virtual ICollection<EmployeeModel> Employee { get; set; }
    }
}
