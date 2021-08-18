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
using MailKit;
using MimeKit;
using System.Net.Mail;

namespace WebApplication5.Controllers
{
    public class TblphotographersController : Controller
    {
        private readonly exportdataContext _context;

        public TblphotographersController(exportdataContext context)
        {
            _context = context;
        }
        public IActionResult photographerlogin()
        {
            return View();
        }
        public IActionResult cedit()
        {
            return RedirectToAction(nameof(Edit),"Tblcustomers");
        }
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult book()
        {
            return View();
        }
        public IActionResult addgallery()
        {
            ViewData["Photoid"] = new SelectList(_context.Tblcategory, "Categoryid", "Categoryname");
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addgallery(IFormFile files, [Bind("Photoid,Photourl,Photographerid,Customerid,Categoryid")] Tblphoto tblphoto)
        {
            if (ModelState.IsValid)
            {

                var fileName = Path.GetFileName(files.FileName);
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                var extension = Path.GetExtension(fileName);
                var newFileName = String.Concat(myUniqueFileName, extension);
                tblphoto.Photourl = newFileName;
                var filepath =
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img","gallery")).Root + $@"\{newFileName}";
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    files.CopyTo(fs);
                    fs.Flush();
                }
                var name = HttpContext.Session.GetString("puname");
                var tblcust = await _context.Tblphotographer
                      .FirstOrDefaultAsync(m => m.Photographername == name);


                if (tblcust == null)
                {
                    return RedirectToAction(nameof(photographerlogin));
                }
                tblphoto.Photographerid = tblcust.Photographerid;
                _context.Add(tblphoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Photoid"] = new SelectList(_context.Tblcategory, "Categoryid", "Categoryname");
            return View(tblphoto);
        }

        public async Task<IActionResult> moreinfo(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photographer = await _context.Tblphotographer
                .Include(t=>t.Tblpackage)
                .FirstOrDefaultAsync(m => m.Photographerid == id);
            var  photo = await _context.Tblphoto
               .FirstOrDefaultAsync(m => m.Photographerid == id);
            if (photographer == null)
            {
                return NotFound();
            }
            ViewData["Message"] = photo;
       
            return View(photographer);
        }

        [HttpPost]
        public async Task<IActionResult> photographerlogin(Tblphotographer p)
        {
            if (p.Phusername == null)
            {
                return NotFound();
            }

            var photographer = await _context.Tblphotographer
                  .FirstOrDefaultAsync(m => m.Phusername == p.Phusername);
            var photographer2 = await _context.Tblphotographer
                 .FirstOrDefaultAsync(m => m.Phpassword == p.Phpassword);
            if (photographer == null || photographer2 == null)
            {
                return RedirectToAction(nameof(photographerlogin));
            }
            else
            {
                 HttpContext.Session.SetString("puname", p.Phusername);
             
                return RedirectToAction(nameof(Index));
            }

        }
        // GET: Tblphotographers
        public async Task<IActionResult> Index()
        {
            var exportdataContext = _context.Tblphotographer.Include(t => t.City).ToList();
            ViewBag.result = exportdataContext;
            return View(exportdataContext);
        }

        // GET: Tblphotographers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblphotographer = await _context.Tblphotographer
                .Include(t => t.City)
                .FirstOrDefaultAsync(m => m.Photographerid == id);
            if (tblphotographer == null)
            {
                return NotFound();
            }

            return View(tblphotographer);
        }

        // GET: Tblphotographers/Create
        public IActionResult Create()
        {
            ViewData["Cityid"] = new SelectList(_context.Tblcity, "Cityid", "Cityname");
            return View();
        }

        // POST: Tblphotographers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile files,[Bind("Photographerid,Photographername,Phusername,Phpassword,Contact,Phprofile,Phmail,Cityid,Lat,Lan")] Tblphotographer tblphotographer)
        {
            if (ModelState.IsValid)
            {
              
                    var fileName = Path.GetFileName(files.FileName);
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        var extension = Path.GetExtension(fileName);
                        var newFileName = String.Concat(myUniqueFileName, extension);
                        tblphotographer.Phprofile = newFileName;
                        var filepath =
                        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")).Root + $@"\{newFileName}";
                        using (FileStream fs = System.IO.File.Create(filepath))
                        {
                            files.CopyTo(fs);
                            fs.Flush();
                        }

                MailMessage mm = new MailMessage();
                mm.To.Add(tblphotographer.Phmail);
                mm.Subject = "Welcome";
                mm.Body = "Welcome to Pictick";
                mm.From = new MailAddress("kishan.vala.it.27@gmail.com");
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("kishan.vala.it.27@gmail.com", "9081010313");
                smtp.Send(mm);
                _context.Add(tblphotographer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cityid"] = new SelectList(_context.Tblcity, "Cityid", "Cityid", tblphotographer.Cityid);
            return View(tblphotographer);
        }

        // GET: Tblphotographers/Edit/5
        public async Task<IActionResult> Edit()
        {
            var name = HttpContext.Session.GetString("puname");
            var tblpht = await _context.Tblphotographer
                  .FirstOrDefaultAsync(m => m.Phusername== name);


            if (tblpht == null)
            {
                return RedirectToAction(nameof(photographerlogin));
            }

            int id = tblpht.Photographerid;
            var tblphotographer = await _context.Tblphotographer.FindAsync(id);
            if (tblphotographer == null)
            {
                return NotFound();
            }
         
            ViewData["Cityid"] = new SelectList(_context.Tblcity, "Cityid", "Cityname", tblphotographer.Cityid);
            return View(tblphotographer);
        }

        // POST: Tblphotographers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("Photographerid,Photographername,Phusername,Phpassword,Contact,Phprofile,Phmail,Cityid,Lat,Lan")] Tblphotographer tblphotographer)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblphotographer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblphotographerExists(tblphotographer.Photographerid))
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
            ViewData["Cityid"] = new SelectList(_context.Tblcity, "Cityid", "Cityid", tblphotographer.Cityid);
            return View(tblphotographer);
        }

        // GET: Tblphotographers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblphotographer = await _context.Tblphotographer
                .Include(t => t.City)
                .FirstOrDefaultAsync(m => m.Photographerid == id);
            if (tblphotographer == null)
            {
                return NotFound();
            }

            return View(tblphotographer);
        }

        // POST: Tblphotographers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblphotographer = await _context.Tblphotographer.FindAsync(id);
            _context.Tblphotographer.Remove(tblphotographer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblphotographerExists(int id)
        {
            return _context.Tblphotographer.Any(e => e.Photographerid == id);
        }
    }
}
