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
using System.Globalization;
using System.Threading;

namespace Smart_Electrician_Support_System.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DbConnectionClass _context;
        private ProductsService _service;
        private ProductCategoryService _PrCatService;


        public ProductsController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _service = new ProductsService(context, mapper);
            _PrCatService = new ProductCategoryService(context, mapper);

            //CultureInfo ci = new CultureInfo("si-LK");
            //ci.NumberFormat.CurrencySymbol = "Rs ";
            //Thread.CurrentThread.CurrentCulture = ci;

        }


        // GET: Products
        public IActionResult Index()
        {

            ViewData["NewID"] = ProductsService.NewID();
            ViewData["PrCategoryList"] = new SelectList(_PrCatService.GetList(), "PrdCat_ID", "PrdCat_Type");
            var GetList = ProductsService.GetList();
            return View(GetList);
        }

        // GET: Products/Details/5
        public IActionResult Details(string id)
        {
            try
            {
                var Data = ProductsService.Find(id);
                return View(Data);
            }
            catch (Exception er)
            {
                er.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Products/Create
        public IActionResult CreatePartial()
        {
            ViewData["NewID"] = ProductsService.NewID();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    collection.PrID = ProductsService.NewID();
                    bool AddData = ProductsService.Add(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Products", "Data Successfully Added.");
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

        // GET: Products/Edit/5
        public IActionResult Edit(string id)
        {
            try
            {

                var Data = ProductsService.Find(id);
                return View(Data);
            }
            catch (Exception err)
            {
                err.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await ProductsService.Update(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "Products", "Data Successfully Updated.");
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

        // GET: Products/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productsModel = await _context.ProductsModel
        //        .FirstOrDefaultAsync(m => m.PrID == id);
        //    if (productsModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productsModel);
        //}

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<string> DeleteConfirmed(string id)
        {
            try
            {
                if (id != null)
                {


                    bool AddData = await ProductsService.Delete(id);

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
