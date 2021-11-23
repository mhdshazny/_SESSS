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
    public class ProductCategoryController : Controller
    {
        private readonly DbConnectionClass context;
        private readonly IMapper mapper;
        private readonly ProductCategoryService service;

        public ProductCategoryController(DbConnectionClass context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            service = new ProductCategoryService(context, mapper);
        }

        public IActionResult Index()
        {
            ViewData["NewID"] = service.NewID();
            return View(service.GetList());
        }






        // GET: ProductCategoryController/Details/5
        public ActionResult Details(int id)
        {
            var empCatV = ProductCategoryService.FindPrCat(id);

            return View(empCatV);
        }


        // POST: ProductCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    bool AddData = ProductCategoryService.AddData(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "ProductCategory", "Data Successfully Added.");
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

        // GET: ProductCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return RedirectToAction(nameof(Index));
            }
            var empCatV = ProductCategoryService.FindPrCat(id);

            return View(empCatV);
        }

        // POST: ProductCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await ProductCategoryService.UpdData(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "ProductCategory", "Data Successfully Updated.");
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

        // GET: ProductCategoryController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ProductCategoryController/Delete/5
        [HttpPost]
        public async Task<string> Delete(int id)
        {
            try
            {
                if (id >=0)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await ProductCategoryService.DelData(id);

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
