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

        public async Task<IActionResult> LookupById(string companyId)
        {
            if (string.IsNullOrWhiteSpace(companyId))
            {
                return RedirectToAction("Index");
            }

            int searchCompanyId;
             if (!int.TryParse(companyId, out searchCompanyId))
            {
                return Problem($"'{companyId}' is not a valid Company ID");
            }

            return _context.FlowpointSupportCompanies != null ?
                        View("Index", await _context.FlowpointSupportCompanies
                            .Where(fsc =>  fsc.ICompanyId == searchCompanyId && !fsc.BIsDeleted)
                            .ToListAsync()) :
                        Problem("Entity set 'FlowpointContext.FlowpointSupportCompanies'  is null.");
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction("Index");
            }

            // Note: EF Expression Trees do not allow the null propogation operator (?), so using ternary operator instead
            return _context.FlowpointSupportCompanies != null ?
                        View("Index", await _context.FlowpointSupportCompanies
                            .Where(fsc => (
                                            fsc.VCompanyName.Contains(searchTerm) ||
                                            fsc.VStreet1.Contains(searchTerm) ||
                                            (fsc.VStreet2 != null ? fsc.VStreet2.Contains(searchTerm) : false) ||
                                            (fsc.VCity != null ? fsc.VCity.Contains(searchTerm) : false) ||
                                            (fsc.VProvince != null ? fsc.VProvince.Contains(searchTerm) : false) ||
                                            (fsc.VPostalCode != null ? fsc.VPostalCode.Contains(searchTerm) : false) ||
                                            (fsc.VCountry != null ? fsc.VCountry.Contains(searchTerm) : false) ||
                                            (fsc.VContact != null ? fsc.VContact.Contains(searchTerm) : false) ||
                                            (fsc.VPhone != null ? fsc.VPhone.Contains(searchTerm) : false) ||
                                            (fsc.VFax != null ? fsc.VFax.Contains(searchTerm) : false) ||
                                            (fsc.VEmail != null ? fsc.VEmail.Contains(searchTerm) : false)
                                          ) &&
                                          !fsc.BIsDeleted)
                            .ToListAsync()) :
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
