using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class DashTargetViewModel
    {
        public int id { get; set; }
        public int monthTarget { get; set; }
        public int yearTarget { get; set; }
        public decimal revenueAnnum { get; set; }
        public decimal revenueMonth { get; set; }
        public string CEOMessage { get; set; }
        public string specialWishes { get; set; }
        public string status { get; set; }
    }
}
