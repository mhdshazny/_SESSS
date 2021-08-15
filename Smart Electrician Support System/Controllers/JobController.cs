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

        public JobController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _service = new JobService(context, mapper);
        }

        // GET: Job
        public IActionResult Index()
        {
            ViewData["NewID"] = JobService.NewID();

            var GetList = JobService.GetList();
            return View(GetList);
        }

        // GET: Job/Details/5
        public IActionResult Details(string id)
        {
            try
            {
                var Data = JobService.Find(id);
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
            ViewData["NewID"] = JobService.NewID();
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.Appo_ID = JobService.NewID();
                    bool AddData = JobService.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Job", "Data Successfully Added.");
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

        // GET: Job/Edit/5
        public IActionResult Edit(string id)
        {
            try
            {
                var Data = JobService.Find(id);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<string> DeleteConfirmed(string id)
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
