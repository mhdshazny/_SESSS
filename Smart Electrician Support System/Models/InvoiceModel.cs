using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Models
{
    [Table("Tbl_Invoice")]
    public class InvoiceModel
    {
        [Key]
        public string Inv_ID { get; set; }
        public string Inv_GenBy { get; set; }
        public DateTime Inv_Date { get; set; }
        public string Job_ID { get; set; }
        public string Inv_TotAmnt { get; set; }
        public string Inv_PaymentStatus { get; set; }
        public string Inv_Status { get; set; }
    }
}
