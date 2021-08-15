using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.ViewModels
{
    public class AppointmentViewModel
    {
        [DisplayName("Appointment ID")]
        [Required(ErrorMessage = "Please provide a valid ID.")]
        public string Appo_ID { get; set; }
        [DisplayName("Customer ID")]
        [Required(ErrorMessage = "Please provide a valid Customer ID.")]
        public string Cus_ID { get; set; }
        [DisplayName("Subject")]
        [Required(ErrorMessage = "Please provide a Appointment Subject.")]
        public string Appo_Subject { get; set; }
        [DisplayName("Appt. Time")]
        [Required(ErrorMessage = "Please provide a valid Time.")]
        public DateTime Appo_Time { get; set; }
        [DisplayName("Description")]
        [Required(ErrorMessage = "Please provide a valid Description.")]
        public string Appo_Descr { get; set; }
        [DisplayName("Handled By")]
        [Required(ErrorMessage = "Please provide a valid Handler ID.")]
        public string Appo_HandledBy { get; set; }
        [DisplayName("Appt. Status")]
        [Required(ErrorMessage = "Please provide a valid status.")]
        public string Appo_Status { get; set; }
    }
}
