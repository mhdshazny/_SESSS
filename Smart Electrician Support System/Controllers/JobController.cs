using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;

namespace Smart_Electrician_Support_System.Controllers
{
    public class JobController : Controller
    {
        private readonly DbConnectionClass _context;
        private JobService _service;
        private EmployeeService _EmpService;
        private AppointmentService _AppoService;
        private ProductsService _PrService;
        private UsedProductsService _UsedPrService;

        public JobController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _service = new JobService(context, mapper);
            _EmpService = new EmployeeService(context, mapper);
            _AppoService = new AppointmentService(context, mapper);
            _PrService = new ProductsService(context, mapper);
            _UsedPrService = new UsedProductsService(context, mapper);
        }

        // GET: Job
        public IActionResult Index()
        {
            ViewData["NewID"] = JobService.NewID();
            ViewData["AppList"] = new SelectList(AppointmentService.GetPendingList(),"Appo_ID", "Appo_Subject");


            ViewData["EmpList"] = new SelectList(EmployeeService.GetListByType("Labour"), "EmpID","lName");
            ViewData["ElecList"] = new SelectList(EmployeeService.GetListByType("Electrician"),"EmpID","lName");
            ViewData["PrdList"] = new SelectList(ProductsService.GetList(),"PrID","PrName");
            
            

            var GetList = JobService.GetList();
            return View(GetList);
        }

        public IActionResult Create()
        {
            return RedirectToAction("Index");
        }

        // GET: Job/Details/5
        public IActionResult Details(string id)
        {
            try
            {
                var Data = JobService.Find(id);
                ViewBag.DateElapsed = JobService.DateTimeElapsed(id).ToString("%d");
                ViewBag.DaysElapsed = JobService.DaysElapsed(id).ToString("%d");
                ViewBag.UsedPrds = UsedProductsService.GetListByJid(id);
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Job/Create
        public IActionResult CreatePartial()
        {
            ViewData["AppList"] = AppointmentService.GetList();
            ViewData["EmpList"] = EmployeeService.GetList();
            ViewData["ElecList"] = EmployeeService.GetListByType("Electrician");



            ViewData["NewID"] = JobService.NewID();
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> CreateAsync(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    bool AddData = await JobService.AddAsync(collection);

                    if (AddData)
                    {
                        return RedirectToAction("Index", "Job", "Data Successfully Added.");
                    }
                    else
                        return RedirectToAction("Index", collection);

                }
                else
                {
                    return RedirectToAction("Index", collection);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), collection);
            }
        }

        // GET: Job/Edit/5
        public IActionResult Edit(string id)
        {
            try
            {
                var Data = JobService.Find(id);
                ViewData["AppList"] = new SelectList(AppointmentService.GetPendingList(), "Appo_ID", "Appo_Subject");


                ViewData["EmpList"] = new SelectList(EmployeeService.GetListByType("Labour"), "EmpID", "lName");
                ViewData["ElecList"] = new SelectList(EmployeeService.GetListByType("Electrician"), "EmpID", "lName");
                ViewData["PrdList"] = new SelectList(ProductsService.GetList(), "PrID", "PrName");


                return View(Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await JobService.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Job", "Data Successfully Updated.");
                    }
                    else
                        return RedirectToAction(nameof(Edit), collection);

                }
                else
                {
                    return RedirectToAction(nameof(Edit), collection);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Edit), collection);
            }
        }

        // POST: Job/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<string> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    bool AddData = await JobService.Delete(id);

                    if (AddData)
                    {
                        return "Success";
                    }
                    else
                        return "Failed";

                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

    }
}
