using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TblappointmentstatusController : Controller
    {
        private readonly exportdataContext _context;

        public TblappointmentstatusController(exportdataContext context)
        {
            _context = context;
        }

        // GET: Tblappointmentstatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tblappointmentstatus.ToListAsync());
        }

        // GET: Tblappointmentstatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointmentstatus = await _context.Tblappointmentstatus
                .FirstOrDefaultAsync(m => m.Statusid == id);
            if (tblappointmentstatus == null)
            {
                return NotFound();
            }

            return View(tblappointmentstatus);
        }

        // GET: Tblappointmentstatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tblappointmentstatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Statusid,Appstatus")] Tblappointmentstatus tblappointmentstatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblappointmentstatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblappointmentstatus);
        }

        // GET: Tblappointmentstatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointmentstatus = await _context.Tblappointmentstatus.FindAsync(id);
            if (tblappointmentstatus == null)
            {
                return NotFound();
            }
            return View(tblappointmentstatus);
        }

        // POST: Tblappointmentstatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Statusid,Appstatus")] Tblappointmentstatus tblappointmentstatus)
        {
            if (id != tblappointmentstatus.Statusid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblappointmentstatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblappointmentstatusExists(tblappointmentstatus.Statusid))
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
            return View(tblappointmentstatus);
        }

        // GET: Tblappointmentstatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointmentstatus = await _context.Tblappointmentstatus
                .FirstOrDefaultAsync(m => m.Statusid == id);
            if (tblappointmentstatus == null)
            {
                return NotFound();
            }

            return View(tblappointmentstatus);
        }

        // POST: Tblappointmentstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblappointmentstatus = await _context.Tblappointmentstatus.FindAsync(id);
            _context.Tblappointmentstatus.Remove(tblappointmentstatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblappointmentstatusExists(int id)
        {
            return _context.Tblappointmentstatus.Any(e => e.Statusid == id);
        }
    }
}
