using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;

namespace Smart_Electrician_Support_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly DbConnectionClass _context;
        private readonly AppointmentService _service;
        private readonly EmployeeService _empService;
        private readonly CustomerService _cusService;

        public AppointmentController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _service = new AppointmentService(context, mapper);
            _empService = new EmployeeService(context, mapper);
            _cusService = new CustomerService(context, mapper);
        }

        // GET: Appointment
        public IActionResult Index()
        {

            ViewData["NewID"] = AppointmentService.NewID();

            List<EmployeeViewModel> empLi = empLiDDL(EmployeeService.GetListExceptElectr());
            ViewData["EmpList"] = new SelectList(empLi, "EmpID", "lName");

            List<CustomerViewModel> cusLi = cusLiDDL(CustomerService.GetList());
            ViewData["CusList"] = new SelectList(cusLi, "CusID", "CuslName"); ;

            var GetList = AppointmentService.GetList();
            return View(GetList);
        }

        public List<EmployeeViewModel> empLiDDL(List<EmployeeViewModel> empLi)
        {
            foreach (var item in empLi)
            {
                item.lName = item.fName+" "+ item.lName + " (" + item.EmpID + ")";
            }
            return empLi;
        }


        public List<CustomerViewModel> cusLiDDL(List<CustomerViewModel> cusLi)
        {
            foreach (var item in cusLi)
            {
                item.CuslName = item.CusfName + " " + item.CuslName + " (" + item.CusID + ")";
            }
            return cusLi;
        }

        // GET: Appointment/Details/5
        public IActionResult Details(string id)
        {
            try
            {
                var Data = AppointmentService.Find(id);
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            ViewData["NewID"] = AppointmentService.NewID();
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppointmentViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.Appo_ID = AppointmentService.NewID();
                    bool AddData = AppointmentService.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Appointment", "Data Successfully Added.");
                    }
                    else
                        return RedirectToAction(nameof(Index), collection);

                }
                else
                {
                    return RedirectToAction(nameof(Index), collection);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), collection);
            }
        }

        // GET: Appointment/Edit/5
        public IActionResult Edit(string id)
        {
            try
            {

                var Data = AppointmentService.Find(id);
                return View(Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await AppointmentService.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Appointment", "Data Successfully Updated.");
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

        // GET: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<string> DeleteConfirmed(string id)
        {
            try
            {
                if (id != null)
                {


                    bool AddData = await AppointmentService.Delete(id);

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
