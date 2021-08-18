using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TblappointmentslotsController : Controller
    {
        private readonly exportdataContext _context;

        public TblappointmentslotsController(exportdataContext context)
        {
            _context = context;
        }

        // GET: Tblappointmentslots
        public async Task<IActionResult> Index()
        {
            var name = HttpContext.Session.GetString("puname");
            var tbl = await _context.Tblphotographer
                  .FirstOrDefaultAsync(m => m.Phusername == name);

            IEnumerable<Tblappointmentslot> s = (from photo in _context.Tblappointmentslot //join comment in exportdatacontext.Tblphotocomment on photo.Photoid equals comment.Photoid
                                                                   where photo.Photographerid ==tbl.Photographerid
                                                                   select new Tblappointmentslot()
                                                                   {
                                                                       Slotid = photo.Slotid,
                                                                     
                                                                       Photographerid = photo.Photographerid,
                                                                       Duration = photo.Duration,
                                                                       //Commentid=comment.Commentid,
                                                                       //Customerid=comment.Customerid,
                                                                       //Comment=comment.Comment,
                                                                       //Commentdate=comment.Commentdate,

                                                                   }).ToList();
          
            return View(s);
        }

        // GET: Tblappointmentslots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointmentslot = await _context.Tblappointmentslot
                .Include(t => t.Photographer)
                .FirstOrDefaultAsync(m => m.Slotid == id);
            if (tblappointmentslot == null)
            {
                return NotFound();
            }

            return View(tblappointmentslot);
        }

        // GET: Tblappointmentslots/Create
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: Tblappointmentslots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Slotid,Photographerid,Duration")] Tblappointmentslot tblappointmentslot)
        {
            if (ModelState.IsValid)
            {
                var name = HttpContext.Session.GetString("puname");
                var tbl = await _context.Tblphotographer
                      .FirstOrDefaultAsync(m => m.Phusername == name);

                tblappointmentslot.Photographerid = tbl.Photographerid;
                _context.Add(tblappointmentslot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Tblphotographers");
            }
         
            return View(tblappointmentslot);
        }

        // GET: Tblappointmentslots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointmentslot = await _context.Tblappointmentslot.FindAsync(id);
            if (tblappointmentslot == null)
            {
                return NotFound();
            }
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblappointmentslot.Photographerid);
            return View(tblappointmentslot);
        }

        // POST: Tblappointmentslots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Slotid,Photographerid,Duration")] Tblappointmentslot tblappointmentslot)
        {
            if (id != tblappointmentslot.Slotid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblappointmentslot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblappointmentslotExists(tblappointmentslot.Slotid))
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
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblappointmentslot.Photographerid);
            return View(tblappointmentslot);
        }

        // GET: Tblappointmentslots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointmentslot = await _context.Tblappointmentslot
                .Include(t => t.Photographer)
                .FirstOrDefaultAsync(m => m.Slotid == id);
            if (tblappointmentslot == null)
            {
                return NotFound();
            }

            return View(tblappointmentslot);
        }

        // POST: Tblappointmentslots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblappointmentslot = await _context.Tblappointmentslot.FindAsync(id);
            _context.Tblappointmentslot.Remove(tblappointmentslot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblappointmentslotExists(int id)
        {
            return _context.Tblappointmentslot.Any(e => e.Slotid == id);
        }
    }
}
