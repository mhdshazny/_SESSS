using AutoMapper;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class InvoiceService
    {
        private DbConnectionClass _context;
        private IMapper _mapper;
        private UsedProductsService _usdProdsService;

        public InvoiceService(DbConnectionClass context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            _usdProdsService = new UsedProductsService(context,mapper);
        }

        internal InvoiceViewModel GetInvoice(string id)
        {
            try
            {
                var data = _context.InvoiceData.Where(i => i.Job_ID == id).FirstOrDefault();

                InvoiceViewModel obj = _mapper.Map<InvoiceViewModel>(data);

                obj.UsedPrds = UsedProductsService.GetListByJid(obj.Job_ID);

                var JobData = _context.JobData.Where(i => i.Job_ID == id).FirstOrDefault();
                var customerID = _context.AppointmentData.Where(i => i.Appo_ID == JobData.Appo_ID).Select(x => x.Cus_ID).FirstOrDefault();
                var cusInfo = _context.CustomerData.Where(i=>i.CusID==customerID).FirstOrDefault();

                obj.CustomerData = _mapper.Map<CustomerViewModel>(cusInfo);
                obj.JobSubject = JobData.Job_Subject;
                return obj;

            }
            catch (Exception er)
            {
                return new InvoiceViewModel();
            }

        }
    }
}
