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
    public class CustomerController : Controller
    {
        private readonly DbConnectionClass _context;
        private CustomerService _service;

        public CustomerController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _service = new CustomerService(context,mapper);
        }

        // GET: Customer
        public IActionResult Index()
        {
            ViewData["NewID"] = CustomerService.NewID();

            var GetList = CustomerService.GetList();
            return View(GetList);
        }

        // GET: Customer/Details/5
        public IActionResult Details(string id)
        {
            try
            {
                var Data = CustomerService.Find(id);
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["NewID"] = CustomerService.NewID();
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.CusID = CustomerService.NewID();
                    bool AddData = CustomerService.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Customer", "Data Successfully Added.");
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

        // GET: Customer/Edit/5
        public IActionResult Edit(string id)
        {
            try
            {

                var Data = CustomerService.Find(id);
                return View(Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));

            }
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel collection)
        {
            try
            {
                if (collection != null)
                {

                    bool AddData = await CustomerService.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Customer", "Data Successfully Updated.");
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


                    bool AddData = await CustomerService.Delete(id);

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
