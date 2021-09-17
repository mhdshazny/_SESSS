﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class InvoiceViewModel
    {
        [DisplayName("Invoice ID")]
        public string Inv_ID { get; set; }
        [DisplayName("Generated By")]
        public string Inv_GenBy { get; set; }
        [DisplayName("Invoice Date")]
        [DataType(DataType.DateTime)]
        public DateTime Inv_Date { get; set; }
        [DisplayName("Job ID")]
        public string Job_ID { get; set; }
        [DisplayName("Total Amount")]
        [DataType(DataType.Currency)]
        public double Inv_TotAmnt { get; set; }
        [DisplayName("Payment Status")]
        public string Inv_PaymentStatus { get; set; }
        [DisplayName("Invoice Status")]
        public string Inv_Status { get; set; }

        [NotMapped]
        public List<UsedProductsViewModel> UsedPrds { get; set; }

        [NotMapped]
        public string JobSubject { get; set; }
        [NotMapped]
        public CustomerViewModel CustomerData { get; set; }
    }
}
