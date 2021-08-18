using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TblphotoesController : Controller
    {
        private readonly exportdataContext _context;

        public TblphotoesController(exportdataContext context)
        {
            _context = context;
        }

        // GET: Tblphotoes
        public async Task<IActionResult> Index()
        {
            var exportdataContext = _context.Tblphoto.Include(t => t.Category).Include(t => t.Customer).Include(t => t.Photographer);
            return View(await exportdataContext.ToListAsync());
        }

        // GET: Tblphotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblphoto = await _context.Tblphoto
                .Include(t => t.Category)
                .Include(t => t.Customer)
                .Include(t => t.Photographer)
                .FirstOrDefaultAsync(m => m.Photoid == id);
            if (tblphoto == null)
            {
                return NotFound();
            }

            return View(tblphoto);
        }

        // GET: Tblphotoes/Create
        public IActionResult Create()
        {
            ViewData["Categoryid"] = new SelectList(_context.Tblcategory, "Categoryid", "Categoryname");
          //  ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid");
          //  ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid");
            return View();
        }

        // POST: Tblphotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile files,[Bind("Photoid,Photourl,Photographerid,Customerid,Categoryid")] Tblphoto tblphoto)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(files.FileName);
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                var extension = Path.GetExtension(fileName);
                var newFileName = String.Concat(myUniqueFileName, extension);
                tblphoto.Photourl = newFileName;
                var filepath =
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "gallery")).Root + $@"\{newFileName}";
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    files.CopyTo(fs);
                    fs.Flush();
                }
                var name = HttpContext.Session.GetString("puname");
                var tblcust = await _context.Tblphotographer
                      .FirstOrDefaultAsync(m => m.Phusername == name);


                if (tblcust == null)
                {
                    return RedirectToAction(nameof(Index),"Tblphotographers");
                }
                tblphoto.Photographerid = tblcust.Photographerid;
                _context.Add(tblphoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Tblphotographers");
            }
            ViewData["Categoryid"] = new SelectList(_context.Tblcategory, "Categoryid", "Categoryid", tblphoto.Categoryid);
          //  ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid", tblphoto.Customerid);
           // ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblphoto.Photographerid);
            return View(tblphoto);
        }

        // GET: Tblphotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblphoto = await _context.Tblphoto.FindAsync(id);
            if (tblphoto == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Tblcategory, "Categoryid", "Categoryid", tblphoto.Categoryid);
            ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid", tblphoto.Customerid);
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblphoto.Photographerid);
            return View(tblphoto);
        }

        // POST: Tblphotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Photoid,Photourl,Photographerid,Customerid,Categoryid")] Tblphoto tblphoto)
        {
            if (id != tblphoto.Photoid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblphoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblphotoExists(tblphoto.Photoid))
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
            ViewData["Categoryid"] = new SelectList(_context.Tblcategory, "Categoryid", "Categoryid", tblphoto.Categoryid);
            ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid", tblphoto.Customerid);
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblphoto.Photographerid);
            return View(tblphoto);
        }

        // GET: Tblphotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblphoto = await _context.Tblphoto
                .Include(t => t.Category)
                .Include(t => t.Customer)
                .Include(t => t.Photographer)
                .FirstOrDefaultAsync(m => m.Photoid == id);
            if (tblphoto == null)
            {
                return NotFound();
            }

            return View(tblphoto);
        }

        // POST: Tblphotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblphoto = await _context.Tblphoto.FindAsync(id);
            _context.Tblphoto.Remove(tblphoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblphotoExists(int id)
        {
            return _context.Tblphoto.Any(e => e.Photoid == id);
        }
    }
}
