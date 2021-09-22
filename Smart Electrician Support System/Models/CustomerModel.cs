using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_Customer")]
    public class CustomerModel
    {
        [Key]
        public string CusID { get; set; }
        public string CusfName { get; set; }
        public string CuslName { get; set; }
        public string CusNIC { get; set; }
        public string CusGender { get; set; }
        public string CusAddress { get; set; }
        public string CusContact { get; set; }
        public string CusEmail { get; set; }
        public string CusPassw { get; set; }
        public string CusStatus { get; set; }
        public string CusProperty { get; set; }
    }
}