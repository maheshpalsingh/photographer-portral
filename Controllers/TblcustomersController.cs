using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TblcustomersController : Controller
    {
        private readonly exportdataContext _context;

        public TblcustomersController(exportdataContext context)
        {
            _context = context;
        }
        public IActionResult customerlogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> customerlogin(Tblcustomer p)
        {
            if (p.Cuusername == null)
            {
                return NotFound();
            }

            var photographer = await _context.Tblcustomer
                  .FirstOrDefaultAsync(m => m.Cuusername == p.Cuusername);
            var photographer2 = await _context.Tblcustomer
                 .FirstOrDefaultAsync(m => m.Cupassword == p.Cupassword);
            if (photographer == null || photographer2 == null)
            {
                return RedirectToAction(nameof(customerlogin));
            }
            else
            {
              

                HttpContext.Session.SetString("cuname", p.Cuusername);
              
                return RedirectToAction(nameof(Index),"Tblphotographers");
            }

        }
        // GET: Tblcustomers
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Tblcustomer.ToListAsync());
        }

        // GET: Tblcustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblcustomer = await _context.Tblcustomer
                .FirstOrDefaultAsync(m => m.Customerid == id);
            if (tblcustomer == null)
            {
                return NotFound();
            }

            return View(tblcustomer);
        }

        // GET: Tblcustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tblcustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile files,[Bind("Customerid,Customername,Cuusername,Cupassword,Contact,Cuprofile,Cumail")] Tblcustomer tblcustomer)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(files.FileName);
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                var extension = Path.GetExtension(fileName);
                var newFileName = String.Concat(myUniqueFileName, extension);
                tblcustomer.Cuprofile = newFileName;
                var filepath =
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")).Root + $@"\{newFileName}";
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    files.CopyTo(fs);
                    fs.Flush();
                }
                MailMessage mm = new MailMessage();
                mm.To.Add(tblcustomer.Cumail);
                mm.Subject = "Welcome";
                mm.Body = "Welcome to Pictick";
                mm.From = new MailAddress("kishan.vala.it.27@gmail.com");
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("kishan.vala.it.27@gmail.com", "9081010313");
                smtp.Send(mm);
                _context.Add(tblcustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(customerlogin));
            }
            return View(tblcustomer);
        }
   

        // GET: Tblcustomers/Edit/5
        public async Task<IActionResult> Edit()
        {
         var name=   HttpContext.Session.GetString("cuname");
            var tblcust = await _context.Tblcustomer
                  .FirstOrDefaultAsync(m => m.Cuusername ==name);


            if (tblcust==null)
            {
                return RedirectToAction(nameof(customerlogin));
            }
           
            int id = tblcust.Customerid;
            var tblcustomer = await _context.Tblcustomer.FindAsync(id);
            if (tblcustomer == null)
            {
                return NotFound();
            }
            return View(tblcustomer);
        }

        // POST: Tblcustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [Bind("Customerid,Customername,Cuusername,Cupassword,Contact,Cuprofile,Cumail")] Tblcustomer tblcustomer)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblcustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblcustomerExists(tblcustomer.Customerid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),"Tblphotographers");
            }
            return View(tblcustomer);
        }

        // GET: Tblcustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblcustomer = await _context.Tblcustomer
                .FirstOrDefaultAsync(m => m.Customerid == id);
            if (tblcustomer == null)
            {
                return NotFound();
            }

            return View(tblcustomer);
        }

        // POST: Tblcustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblcustomer = await _context.Tblcustomer.FindAsync(id);
            _context.Tblcustomer.Remove(tblcustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblcustomerExists(int id)
        {
            return _context.Tblcustomer.Any(e => e.Customerid == id);
        }
    }
}
