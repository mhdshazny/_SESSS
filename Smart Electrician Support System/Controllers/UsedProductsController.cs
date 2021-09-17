using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.ViewModels;

namespace Smart_Electrician_Support_System.Controllers
{
    public class UsedProductsController : Controller
    {
        private readonly DbConnectionClass _context;
        private UsedProductsService _service;
        private ProductsService _prdService;
        private JobService _jobService;
        public UsedProductsController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _service = new UsedProductsService(context, mapper);
            _prdService = new ProductsService(context, mapper);
            _jobService = new JobService(context, mapper);
        }

        // GET: UsedProducts
        public IActionResult Index()
        {
            ViewData["NewID"] = UsedProductsService.NewID();
            ViewData["PrdList"] = new SelectList(ProductsService.GetList(), "PrID", "PrName");

            if (HttpContext.Session.GetString("SessionEmpRole") == "Admin" || HttpContext.Session.GetString("SessionEmpRole") == "Manager")
            {
                //var data = JobService.GetListForDD();
                
                ViewData["JobList"] = new SelectList(JobService.GetListForDD(), "Job_ID", "Job_Subject");
                var GetList = UsedProductsService.GetList();
                return View(GetList);

            }
            else
            {
                var id = HttpContext.Session.GetString("SessionEmpID");
                ViewData["JobList"] = new SelectList(JobService.GetListForElectrician(id), "Job_ID", "Job_Subject");
                var GetList = UsedProductsService.GetListForElectrician(id);
                return View(GetList);
            }



        }

        // GET: UsedProducts/Details/5
        public IActionResult Details(string id)
        {
            try
            {
                var Data = UsedProductsService.Find(id);
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UsedProducts/Create
        public IActionResult Create()
        {
            ViewData["NewID"] = UsedProductsService.NewID();
            return View();
        }

        // POST: UsedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsedProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.Pr_Used_ID = UsedProductsService.NewID();
                    bool AddData = UsedProductsService.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "UsedProducts", "Data Successfully Added.");
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

        // GET: UsedProducts/Edit/5
        public IActionResult Edit(string id)
        {
            try
            {

                var Data = UsedProductsService.Find(id);
                return View(Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

        // POST: UsedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsedProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await UsedProductsService.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "UsedProducts", "Data Successfully Updated.");
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


        [HttpPost, ActionName("Delete")]
        public async Task<string> DeleteConfirmed(string id)
        {
            try
            {
                if (id != null)
                {


                    bool AddData = await UsedProductsService.Delete(id);

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
