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
    [Route("Companies/{companyId:int}/Vendors/")]
    public class VendorsController : Controller
    {
        private readonly FlowpointContext _context;

        public VendorsController(FlowpointContext context)
        {
            _context = context;
        }

        // GET: Vendors
        public async Task<IActionResult> Index(int companyId)
        {
            var flowpointContext = _context.FlowpointSupportVendors
                .Where(fsv => fsv.ICompanyId == companyId &&
                              !fsv.BIsDeleted)
                .Include(f => f.ICompany);

            return View(await flowpointContext.ToListAsync());
        }

        // GET: Vendors/Details/5
        [Route("Details/{vendorId}")]
        public async Task<IActionResult> Details(int companyId, int? vendorId)
        {
            if (vendorId == null || 
                _context.FlowpointSupportVendors == null)
            {
                return NotFound();
            }

            var flowpointSupportVendor = await _context.FlowpointSupportVendors
                .Include(f => f.ICompany)
                .FirstOrDefaultAsync(m => m.IVendorId == vendorId && !m.BIsDeleted);
            if (flowpointSupportVendor == null)
            {
                return NotFound();
            }

            return View(flowpointSupportVendor);
        }

        // GET: Vendors/Create
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["ICompanyId"] = new SelectList(_context.FlowpointSupportCompanies, "ICompanyId", "VCompanyName");
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("IVendorId,ICompanyId,IVendorName,VStreet1,VStreet2,VCity,VProvince,VPostalCode,VCountry,VContact,VPhone,VFax,VEmail,DtCreated,BIsDeleted")] FlowpointSupportVendor flowpointSupportVendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flowpointSupportVendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ICompanyId"] = new SelectList(_context.FlowpointSupportCompanies, "ICompanyId", "VCompanyName", flowpointSupportVendor.ICompanyId);
            return View(flowpointSupportVendor);
        }

        // GET: Vendors/Edit/5
        [Route("Edit/{vendorId}")]
        public async Task<IActionResult> Edit(int? vendorId)
        {
            if (vendorId == null || _context.FlowpointSupportVendors == null)
            {
                return NotFound();
            }

            var flowpointSupportVendor = await _context.FlowpointSupportVendors.FindAsync(vendorId);
            if (flowpointSupportVendor == null)
            {
                return NotFound();
            }
            ViewData["ICompanyId"] = new SelectList(_context.FlowpointSupportCompanies, "ICompanyId", "VCompanyName", flowpointSupportVendor.ICompanyId);
            return View(flowpointSupportVendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{vendorId}")]
        public async Task<IActionResult> Edit(int vendorId, [Bind("IVendorId,ICompanyId,IVendorName,VStreet1,VStreet2,VCity,VProvince,VPostalCode,VCountry,VContact,VPhone,VFax,VEmail,DtCreated,BIsDeleted")] FlowpointSupportVendor flowpointSupportVendor)
        {
            if (vendorId != flowpointSupportVendor.IVendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flowpointSupportVendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowpointSupportVendorExists(flowpointSupportVendor.IVendorId))
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
            ViewData["ICompanyId"] = new SelectList(_context.FlowpointSupportCompanies, "ICompanyId", "VCompanyName", flowpointSupportVendor.ICompanyId);
            return View(flowpointSupportVendor);
        }

        // GET: Vendors/Delete/5
        [Route("Delete/{vendorId}")]
        public async Task<IActionResult> Delete(int? vendorId)
        {
            if (vendorId == null || _context.FlowpointSupportVendors == null)
            {
                return NotFound();
            }

            var flowpointSupportVendor = await _context.FlowpointSupportVendors
                .Include(f => f.ICompany)
                .FirstOrDefaultAsync(m => m.IVendorId == vendorId);
            if (flowpointSupportVendor == null)
            {
                return NotFound();
            }

            return View(flowpointSupportVendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{vendorId}")]
        public async Task<IActionResult> DeleteConfirmed(int vendorId)
        {
            if (_context.FlowpointSupportVendors == null)
            {
                return Problem("Entity set 'FlowpointContext.FlowpointSupportVendors'  is null.");
            }
            var flowpointSupportVendor = await _context.FlowpointSupportVendors.FindAsync(vendorId);
            if (flowpointSupportVendor != null)
            {
                _context.FlowpointSupportVendors.Remove(flowpointSupportVendor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowpointSupportVendorExists(int id)
        {
            return (_context.FlowpointSupportVendors?.Any(e => e.IVendorId == id)).GetValueOrDefault();
        }
    }
}
