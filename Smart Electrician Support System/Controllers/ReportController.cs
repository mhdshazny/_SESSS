using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class ReportController : Controller
    {
        private DbConnectionClass _context;
        private IMapper _mapper;
        private ProductsService _prdService;
        private InvoiceService _invService;
        private JobService _jobService;
        private CustomerService _cusService;
        public ReportController(DbConnectionClass context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _prdService = new ProductsService(context, mapper);
            _invService = new InvoiceService(context, mapper);
            _jobService = new JobService(context, mapper);
            _cusService = new CustomerService(context, mapper);

        }
        public IActionResult Index()
        {
            return new ViewAsPdf();
        }
        public IActionResult StockReport()
        {
            var data = ProductsService.GetList();
            return new ViewAsPdf(data);
        }

        public IActionResult Invoice(string id)
        {
            var data = _invService.GetInvoice(id);
            return new ViewAsPdf(data);
        }

        public IActionResult AllJobsReport()
        {
            var data = JobService.GetListAll();
            return new ViewAsPdf(data);
        }

        public IActionResult AllCusReport()
        {
            var data = CustomerService.GetList();
            return new ViewAsPdf(data);
        }
        public IActionResult AllApptReport()
        {
            var data = AppointmentService.GetList();
            return new ViewAsPdf(data);
        }
        public IActionResult AllElectJobsReport(string id)
        {
            var data = JobService.GetListForElectrician(id);
            return new ViewAsPdf("AllJobsReport", data);
        }
    }
}
