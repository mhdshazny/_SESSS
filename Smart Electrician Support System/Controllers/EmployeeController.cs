using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Controllers
{
    public class EmployeeController : Controller
    {
        private DbConnectionClass _context;
        private readonly IMapper _mapper;
        private EmployeeService _Service;
        private EmpCategoryService _EmpCatService;
        public EmployeeController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _Service = new EmployeeService(context, mapper);
            _EmpCatService = new EmpCategoryService(context, mapper);
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            var GetList = EmployeeService.GetList();
            ViewData["NewID"] = EmployeeService.NewID();
            ViewData["EmpCatList"] = new SelectList(EmpCategoryService.EmpCatList(), "EmpCat_ID", "EmpCat_Type");
            return View(GetList);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                var Data = EmployeeService.Find(id);
                return View(Data);
            }
            catch(Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            ViewData["NewID"] = EmployeeService.NewID();
            ViewData["EmpCatList"] = new SelectList(EmpCategoryService.EmpCatList(), "EmpCat_ID", "EmpCat_Type");
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel collection)
        {

            try
            {
                if (collection != null)
                {
                    collection.EmpID = EmployeeService.NewID();
                    bool AddData = EmployeeService.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Employee", "Data Successfully Added.");
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

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                ViewData["EmpCatList"] = new SelectList(EmpCategoryService.EmpCatList(), "EmpCat_ID", "EmpCat_Type");

                var Data = EmployeeService.Find(id);
                return View(Data);
            }
            catch(Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(EmployeeViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await EmployeeService.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Employee", "Data Successfully Updated.");
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

        // GET: EmployeeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: EmployeeController/Delete/5
        [HttpPost]
        public async Task<string> Delete(string id)
        {
            try
            {
                if (id != null)
                {


                    bool AddData = await EmployeeService.Delete(id);

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
