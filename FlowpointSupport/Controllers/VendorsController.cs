using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowpointSupport.FlowpointDb;
using Humanizer.Localisation.TimeToClockNotation;
using System.ComponentModel.Design;

namespace FlowpointSupport.Controllers
{
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

            ViewBag.CompanyId = companyId;
            return View(await flowpointContext.ToListAsync());
        }

        public async Task<IActionResult> LookupById(string vendorId, int companyId)
        {
            if (string.IsNullOrWhiteSpace(vendorId))
            {
                return RedirectToAction("Index", new { companyId });
            }

            int searchVendorId;
            if (!int.TryParse(vendorId, out searchVendorId))
            {
                return Problem($"'{vendorId}' is not a valid Vendor ID");
            }

            ViewBag.CompanyId = companyId;
            return _context.FlowpointSupportVendors != null ?
                        View("Index", await _context.FlowpointSupportVendors
                            .Where(fsc => fsc.IVendorId == searchVendorId && !fsc.BIsDeleted)
                            .ToListAsync()) :
                        Problem("Entity set 'FlowpointContext.FlowpointSupportVendors'  is null.");
        }

        public async Task<IActionResult> Search(string searchTerm, int companyId)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction("Index", new { companyId });
            }

            ViewBag.CompanyId = companyId;

            // Note: EF Expression Trees do not allow the null propogation operator (?), so using ternary operator instead
            return _context.FlowpointSupportVendors != null ?
                        View("Index", await _context.FlowpointSupportVendors
                            .Where(fsv => (
                                            fsv.VVendorName.Contains(searchTerm) ||
                                            fsv.VStreet1.Contains(searchTerm) ||
                                            (fsv.VStreet2 != null ? fsv.VStreet2.Contains(searchTerm) : false) ||
                                            (fsv.VCity != null ? fsv.VCity.Contains(searchTerm) : false) ||
                                            (fsv.VProvince != null ? fsv.VProvince.Contains(searchTerm) : false) ||
                                            (fsv.VPostalCode != null ? fsv.VPostalCode.Contains(searchTerm) : false) ||
                                            (fsv.VCountry != null ? fsv.VCountry.Contains(searchTerm) : false) ||
                                            (fsv.VContact != null ? fsv.VContact.Contains(searchTerm) : false) ||
                                            (fsv.VPhone != null ? fsv.VPhone.Contains(searchTerm) : false) ||
                                            (fsv.VFax != null ? fsv.VFax.Contains(searchTerm) : false) ||
                                            (fsv.VEmail != null ? fsv.VEmail.Contains(searchTerm) : false)
                                          ) &&
                                          !fsv.BIsDeleted)
                            .ToListAsync()) :
                        Problem("Entity set 'FlowpointContext.FlowpointSupportVendors'  is null.");
        }

        // GET: Vendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || 
                _context.FlowpointSupportVendors == null)
            {
                return NotFound();
            }

            var flowpointSupportVendor = await _context.FlowpointSupportVendors
                .Include(f => f.ICompany)
                .FirstOrDefaultAsync(m => m.IVendorId == id && !m.BIsDeleted);
            if (flowpointSupportVendor == null)
            {
                return NotFound();
            }

            return View(flowpointSupportVendor);
        }

        // GET: Vendors/Create
        public IActionResult Create(int companyId)
        {
            ViewData["ICompanyId"] = new SelectList(_context.FlowpointSupportCompanies, "ICompanyId", "VCompanyName");
            ViewBag.CompanyId = companyId;
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IVendorId,ICompanyId,VVendorName,VStreet1,VStreet2,VCity,VProvince,VPostalCode,VCountry,VContact,VPhone,VFax,VEmail,DtCreated,BIsDeleted")] FlowpointSupportVendor flowpointSupportVendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flowpointSupportVendor);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new
                {
                    controller = "Vendors",
                    action = "Index",
                    companyId = flowpointSupportVendor.ICompanyId
                });
            }
            ViewData["ICompanyId"] = new SelectList(_context.FlowpointSupportCompanies, "ICompanyId", "VCompanyName", flowpointSupportVendor.ICompanyId);
            return View(flowpointSupportVendor);
        }

        // GET: Vendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FlowpointSupportVendors == null)
            {
                return NotFound();
            }

            var flowpointSupportVendor = await _context.FlowpointSupportVendors.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("IVendorId,ICompanyId,VVendorName,VStreet1,VStreet2,VCity,VProvince,VPostalCode,VCountry,VContact,VPhone,VFax,VEmail,DtCreated,BIsDeleted")] FlowpointSupportVendor flowpointSupportVendor)
        {
            if (id != flowpointSupportVendor.IVendorId)
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
                return RedirectToRoute(new
                {
                    controller = "Vendors",
                    action = "Index",
                    companyId = flowpointSupportVendor.ICompanyId
                });
            }
            ViewData["ICompanyId"] = new SelectList(_context.FlowpointSupportCompanies, "ICompanyId", "VCompanyName", flowpointSupportVendor.ICompanyId);
            return View(flowpointSupportVendor);
        }

        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FlowpointSupportVendors == null)
            {
                return NotFound();
            }

            var flowpointSupportVendor = await _context.FlowpointSupportVendors
                .Include(f => f.ICompany)
                .FirstOrDefaultAsync(m => m.IVendorId == id);
            if (flowpointSupportVendor == null)
            {
                return NotFound();
            }

            return View(flowpointSupportVendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FlowpointSupportVendors == null)
            {
                return Problem("Entity set 'FlowpointContext.FlowpointSupportVendors'  is null.");
            }
            var flowpointSupportVendor = await _context.FlowpointSupportVendors.FindAsync(id);

            if (flowpointSupportVendor == null)
            {
                return Problem("Entity 'FlowpointContext.FlowpointSupportVendor' is null.");
            }

            flowpointSupportVendor.BIsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToRoute(new
            {
                controller = "Vendors",
                action = "Index",
                companyId = flowpointSupportVendor.ICompanyId
            });
        }

        private bool FlowpointSupportVendorExists(int id)
        {
            return (_context.FlowpointSupportVendors?.Any(e => e.IVendorId == id)).GetValueOrDefault();
        }
    }
}
