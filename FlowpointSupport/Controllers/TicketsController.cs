using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowpointSupport.FlowpointDb;
using System.ComponentModel.Design;

namespace FlowpointSupport.Controllers
{
    public class TicketsController : Controller
    {
        private readonly FlowpointContext _context;

        public TicketsController(FlowpointContext context)
        {
            _context = context;
        }

        // GET: Tickets
        [Route("Vendors/{vendorId:int}/Tickets/")]
        public async Task<IActionResult> Index(int vendorId, int companyId)
        {
            var flowpointContext = _context.FlowpointSupportTickets
                .Where(fsv => fsv.IVendorId == vendorId &&
                              !fsv.BIsDeleted)
                .Include(f => f.IVendor);
            
            var tickets = await flowpointContext.ToListAsync();

            ViewBag.CompanyId = companyId;
            ViewBag.VendorId = vendorId;

            return View(tickets);
        }

        public async Task<IActionResult> LookupById(string ticketId, int vendorId, int companyId)
        {
            if (string.IsNullOrWhiteSpace(ticketId))
            {
                return RedirectToAction("Index", new { vendorId });
            }

            int searchTicketId;
            if (!int.TryParse(ticketId, out searchTicketId))
            {
                return Problem($"'{ticketId}' is not a valid Ticket ID");
            }

            ViewBag.VendorId = vendorId;
            ViewBag.CompanyId = companyId;
            return _context.FlowpointSupportTickets != null ?
                        View("Index", await _context.FlowpointSupportTickets
                            .Where(fsc => fsc.ITicketId == searchTicketId && !fsc.BIsDeleted)
                            .ToListAsync()) :
                        Problem("Entity set 'FlowpointContext.FlowpointSupportTickets'  is null.");
        }

        public async Task<IActionResult> Search(string searchTerm, int vendorId, int companyId)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction("Index", new { vendorId });
            }

            ViewBag.VendorId = vendorId;

            return _context.FlowpointSupportTickets != null
                        ? View("Index", (await _context.FlowpointSupportTickets
                            
                            .ToListAsync())
                            .Where(fsv => ( // NOTE: by moving this here, the search terms are being evaluated on the server rather than in the database.
                                            //       This is only for testing so that it can search & evaluate the user names, which are hard coded into a dictionary
                                            //       If Users was a real table, it would all be done in the database
                                            fsv.VTicketMessage.Contains(searchTerm) ||
                                            Users.NameById[fsv.ICreatedBy].ToLower().Contains(searchTerm.ToLower()) ||
                                            Users.NameById[fsv.IModifiedBy].ToLower().Contains(searchTerm.ToLower())
                                          ) &&
                                          !fsv.BIsDeleted))
                        : Problem("Entity set 'FlowpointContext.FlowpointSupportTickets' is null.");
        }

        // GET: Tickets/Create
        public IActionResult Create(int vendorId, int companyId)
        {
            ViewData["IVendorId"] = new SelectList(_context.FlowpointSupportVendors, "IVendorId", "VVendorName");
            ViewBag.VendorId = vendorId;
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ITicketId,IVendorId,VTicketMessage,DtCreatedDate,DtModifiedDate,ICreatedBy,IModifiedBy,BIsDeleted")] FlowpointSupportTicket flowpointSupportTicket)
        {
            if (ModelState.IsValid)
            {
                flowpointSupportTicket.IModifiedBy = flowpointSupportTicket.ICreatedBy;
                flowpointSupportTicket.DtModifiedDate = flowpointSupportTicket.DtCreatedDate;
                _context.Add(flowpointSupportTicket);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new
                {
                    controller = "Tickets",
                    action = "Index",
                    vendorId = flowpointSupportTicket.IVendorId
                });
            }
            ViewData["IVendorId"] = new SelectList(_context.FlowpointSupportVendors, "IVendorId", "VVendorName", flowpointSupportTicket.IVendorId);
            return View(flowpointSupportTicket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FlowpointSupportTickets == null)
            {
                return NotFound();
            }

            var flowpointSupportTicket = await _context.FlowpointSupportTickets
                .Include(f => f.IVendor)
                .FirstOrDefaultAsync(t => t.ITicketId == id);

            if (flowpointSupportTicket == null)
            {
                return NotFound();
            }

            ViewData["IVendorId"] = new SelectList(_context.FlowpointSupportVendors, "IVendorId", "VVendorName", flowpointSupportTicket.IVendorId);
            ViewBag.CompanyId = flowpointSupportTicket.IVendor?.ICompanyId;
            ViewBag.VendorId = flowpointSupportTicket.IVendorId;

            return View(flowpointSupportTicket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ITicketId,VTicketMessage,IModifiedBy")] FlowpointSupportTicket updatedTicket)
        {
            if (id != updatedTicket.ITicketId)
            {
                return NotFound();
            }

            var flowpointSupportTicket = _context.FlowpointSupportTickets.First(fst => fst.ITicketId == updatedTicket.ITicketId);

            if (ModelState.IsValid)
            {
                try
                {
                    flowpointSupportTicket.VTicketMessage = updatedTicket.VTicketMessage;
                    flowpointSupportTicket.IModifiedBy = updatedTicket.IModifiedBy;
                    _context.Update(flowpointSupportTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowpointSupportTicketExists(flowpointSupportTicket.ITicketId))
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
                    controller = "Tickets",
                    action = "Index",
                    vendorId = flowpointSupportTicket.IVendorId
                });
            }

            ViewData["IVendorId"] = new SelectList(_context.FlowpointSupportVendors, "IVendorId", "VVendorName", flowpointSupportTicket.IVendorId);
            return View(flowpointSupportTicket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FlowpointSupportTickets == null)
            {
                return NotFound();
            }

            var flowpointSupportTicket = await _context.FlowpointSupportTickets
                .Include(f => f.IVendor)
                .FirstOrDefaultAsync(m => m.ITicketId == id);

            if (flowpointSupportTicket == null)
            {
                return NotFound();
            }

            ViewBag.CompanyId = flowpointSupportTicket.IVendor?.ICompanyId;
            ViewBag.VendorId = flowpointSupportTicket.IVendorId;
            return View(flowpointSupportTicket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FlowpointSupportTickets == null)
            {
                return Problem("Entity set 'FlowpointContext.FlowpointSupportTickets'  is null.");
            }
            var flowpointSupportTicket = await _context.FlowpointSupportTickets.FindAsync(id);
            if (flowpointSupportTicket == null)
            {
                return Problem("Entity 'FlowpointContext.FlowpointSupportTicket'is null.");
            }

            flowpointSupportTicket.BIsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToRoute(new
            {
                controller = "Tickets",
                action = "Index",
                vendorId = flowpointSupportTicket.IVendorId
            });
        }

        private bool FlowpointSupportTicketExists(int id)
        {
            return (_context.FlowpointSupportTickets?.Any(e => e.ITicketId == id)).GetValueOrDefault();
        }
    }
}
