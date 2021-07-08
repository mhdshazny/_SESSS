using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.Services;

namespace Smart_Electrician_Support_System.Controllers
{
    public class EmpTempController : Controller
    {
        private readonly DbConnectionClass _context;

        public EmpTempController(DbConnectionClass context)
        {
            _context = context;
        }

        // GET: EmpTemp
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpIdentityData.ToListAsync());
        }

        // GET: EmpTemp/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empIdentityModel = await _context.EmpIdentityData
                .FirstOrDefaultAsync(m => m.EmpID == id);
            if (empIdentityModel == null)
            {
                return NotFound();
            }

            return View(empIdentityModel);
        }

        // GET: EmpTemp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpTemp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpID,EmpEmail,EmpPassWord,EmpStatus")] EmpIdentityModel empIdentityModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empIdentityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empIdentityModel);
        }

        // GET: EmpTemp/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empIdentityModel = await _context.EmpIdentityData.FindAsync(id);
            if (empIdentityModel == null)
            {
                return NotFound();
            }
            return View(empIdentityModel);
        }

        // POST: EmpTemp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpID,EmpEmail,EmpPassWord,EmpStatus")] EmpIdentityModel empIdentityModel)
        {
            if (id != empIdentityModel.EmpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empIdentityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpIdentityModelExists(empIdentityModel.EmpID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empIdentityModel);
        }

        // GET: EmpTemp/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empIdentityModel = await _context.EmpIdentityData
                .FirstOrDefaultAsync(m => m.EmpID == id);
            if (empIdentityModel == null)
            {
                return NotFound();
            }

            return View(empIdentityModel);
        }

        // POST: EmpTemp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var empIdentityModel = await _context.EmpIdentityData.FindAsync(id);
            _context.EmpIdentityData.Remove(empIdentityModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpIdentityModelExists(string id)
        {
            return _context.EmpIdentityData.Any(e => e.EmpID == id);
        }
    }
}
