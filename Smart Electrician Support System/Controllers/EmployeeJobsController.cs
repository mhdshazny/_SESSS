using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class EmployeeJobsController : Controller
    {
        private readonly DbConnectionClass _context;
        private EmployeeJobsService _service;
        //private EmployeeService _EmpService;
        //private AppointmentService _AppoService;
        //private ProductsService _PrService;

        public EmployeeJobsController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _service = new EmployeeJobsService(context, mapper);
            //_EmpService = new EmployeeService(context, mapper);
            //_AppoService = new AppointmentService(context, mapper);
            //_PrService = new ProductsService(context, mapper);
        }

        public IActionResult Index()
        {
            ViewBag.EmpList = _service.GetNameList();
            ViewBag.Graph = new int[] { 0, 10, 50, 150, 100, 200, 250, 200, 250, 300, 350, 400 };

            return View();
        }

        public IActionResult Index(EmployeeJobsViewModel obj)
        {
            ViewBag.EmpList = _service.GetNameList();
            ViewBag.Graph = new int[] { 0, 10, 50, 150, 100, 200, 250, 200, 250, 300, 350, 400 };
            return View(obj);
        }

        [ActionName("EmpShow")]
        public IActionResult ShowEmpPerf(string id)
        {
            if (id!=null||id!="")
            {
                EmployeeJobsViewModel obj = _service.GetEmpPerf(id);
                ViewBag.Graph =_service.GraphData(id);

                return View("Index",obj);
            }
            else
                return View("Index");
        }
    }
}
