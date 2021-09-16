using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rotativa.AspNetCore;
using SelectPdf;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DbConnectionClass _context;
        private IMapper _mapper;
        private DashboardService _service;
        private EmployeeService _empService;

        public HomeController(ILogger<HomeController> logger,DbConnectionClass context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _service = new DashboardService(context,mapper);
            _empService = new EmployeeService(context,mapper);
        }

        public IActionResult Index()
        {
            //ViewBag.EmpPerfTop = _service.GetTopEmps();

            var obj = _service.Get();
            ViewBag.JobMonthsData = _service.GetJobsMonthData();
            ViewBag.TopEmpList = _service.GetTopEmps();
            ViewBag.PieChart = _service.GetJobStatus();

            return View(obj);
        }

        //public IActionResult EditProfile()
        //{
        //    var empid = HttpContext.Session.GetString("SessionEmpID");
        //    var data = EmployeeService.Find(empid);
        //    return View(data);
        //}

        public IActionResult PrintDash()
        {
            //ViewBag.EmpPerfTop = _service.GetTopEmps();

            var obj = _service.Get();

            return new ViewAsPdf(obj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult GeneratePDF(IFormFile html)
        {
            string _html = html.ToString();
            _html = _html.Replace("StrTag", "<").Replace("EndTag", ">");
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            PdfDocument pdfDoc = htmlToPdf.ConvertHtmlString(_html);
            byte[] pdf = pdfDoc.Save();
            pdfDoc.Close();

            return File(
                pdf,
                "application/pdf",
                "Dashboard.pdf"
                );
        }


        public IActionResult DashConfig()
        {
            var obj = _service.Get();
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(DashTargetViewModel obj)
        {
            bool result = _service.Update(obj);
            if (result==true)
            {
                return RedirectToAction("Index");
            }
            else
                return View("DashConfig");
        }
    }
}
