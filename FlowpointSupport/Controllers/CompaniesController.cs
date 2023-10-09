using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowpointSupport.FlowpointDb;

namespace FlowpointSupport.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly FlowpointContext _context;

        public CompaniesController(FlowpointContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return _context.FlowpointSupportCompanies != null ?
                        View(await _context.FlowpointSupportCompanies.Where(fsc => !fsc.BIsDeleted).ToListAsync()) :
                        Problem("Entity set 'FlowpointContext.FlowpointSupportCompanies'  is null.");
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null ||
                _context.FlowpointSupportCompanies == null)
            {
                return NotFound();
            }

            var flowpointSupportCompany = await _context.FlowpointSupportCompanies
                .FirstOrDefaultAsync(m => m.ICompanyId == id && !m.BIsDeleted);
            if (flowpointSupportCompany == null)
            {
                return NotFound();
            }

            return View(flowpointSupportCompany);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ICompanyId,VCompanyName,VStreet1,VStreet2,VCity,VProvince,VPostalCode,VCountry,VContact,VPhone,VFax,VEmail,DtCreated,BIsDeleted")] FlowpointSupportCompany flowpointSupportCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flowpointSupportCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flowpointSupportCompany);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FlowpointSupportCompanies == null)
            {
                return NotFound();
            }

            var flowpointSupportCompany = await _context.FlowpointSupportCompanies.FindAsync(id);
            if (flowpointSupportCompany == null)
            {
                return NotFound();
            }
            return View(flowpointSupportCompany);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ICompanyId,VCompanyName,VStreet1,VStreet2,VCity,VProvince,VPostalCode,VCountry,VContact,VPhone,VFax,VEmail,DtCreated,BIsDeleted")] FlowpointSupportCompany flowpointSupportCompany)
        {
            if (id != flowpointSupportCompany.ICompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flowpointSupportCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowpointSupportCompanyExists(flowpointSupportCompany.ICompanyId))
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
            return View(flowpointSupportCompany);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FlowpointSupportCompanies == null)
            {
                return NotFound();
            }

            var flowpointSupportCompany = await _context.FlowpointSupportCompanies
                .FirstOrDefaultAsync(m => m.ICompanyId == id);
            if (flowpointSupportCompany == null)
            {
                return NotFound();
            }

            return View(flowpointSupportCompany);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FlowpointSupportCompanies == null)
            {
                return Problem("Entity set 'FlowpointContext.FlowpointSupportCompanies'  is null.");
            }
            var flowpointSupportCompany = await _context.FlowpointSupportCompanies.FindAsync(id);
            if (flowpointSupportCompany != null)
            {
                flowpointSupportCompany.BIsDeleted = true;
                _context.FlowpointSupportCompanies.Update(flowpointSupportCompany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowpointSupportCompanyExists(int id)
        {
            return (_context.FlowpointSupportCompanies?.Any(e => e.ICompanyId == id && !e.BIsDeleted)).GetValueOrDefault();
        }
    }
}
