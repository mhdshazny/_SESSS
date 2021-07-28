using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smart_Electrician_Support_System.Services;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using Smart_Electrician_Support_System.MapperProfiles;
using AutoMapper;

namespace Smart_Electrician_Support_System.Controllers
{
    public class EmpCategoryController : Controller
    {
        private DbConnectionClass _context;
        private readonly IMapper _mapper;
        private EmpCategoryService _Service;

        public EmpCategoryController(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _Service = new EmpCategoryService(context,mapper);
        }
        // GET: EmpCategoryController
        public ActionResult Index()
        {
            var empCatV = EmpCategoryService.EmpCatList();

            return View(empCatV);

        }

        // GET: EmpCategoryController/Details/5
        public ActionResult Details(string id)
        {
            var empCatV = EmpCategoryService.FindEmpCat(id);

            return View(empCatV);
        }

        // GET: EmpCategoryController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EmpCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    bool AddData = EmpCategoryService.AddData(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index),"EmpCategory","Data Successfully Added.") ;
                    }
                    else
                        return RedirectToAction(nameof(Create), collection);
 
                }
                else
                {
                    return RedirectToAction(nameof(Create), collection);
                }
            }
            catch(Exception err)
            {
                return RedirectToAction(nameof(Create), collection);
            }
        }

        // GET: EmpCategoryController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null && EmpCategoryService.AvailCat(id) == false)
            {
                return RedirectToAction(nameof(Index));
            }
            var empCatV = EmpCategoryService.FindEmpCat(id);

            return View(empCatV);
        }

        // POST: EmpCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, EmpCategoryViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await EmpCategoryService.UpdData(collection);

                    if (AddData)
                    {
                        return RedirectToAction(nameof(Index), "EmpCategory", "Data Successfully Updated.");
                    }
                    else
                        return RedirectToAction(nameof(Edit), collection);

                }
                else
                {
                    return RedirectToAction(nameof(Edit), collection);
                }
            }
            catch (Exception err)
            {
                return RedirectToAction(nameof(Edit), collection);
            }
        }

        // GET: EmpCategoryController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: EmpCategoryController/Delete/5
        [HttpPost]
        public async Task<string> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    //if(EmpCategoryService.AvailCat(id)!=true)
                    //    return RedirectToAction("Create","EmpCategory","Invalid ID.");

                    bool AddData = await EmpCategoryService.DelData(id);

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
