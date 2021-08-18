using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TblappointmentsController : Controller
    {
        private readonly exportdataContext _context;

        public TblappointmentsController(exportdataContext context)
        {
            _context = context;
        }

        // GET: Tblappointments
        public async Task<IActionResult> Index()
        {
            var name = HttpContext.Session.GetString("puname");
            var tbl = await _context.Tblphotographer
                  .FirstOrDefaultAsync(m => m.Phusername == name);

            IEnumerable<Tblappointment> s = (from photo in _context.Tblappointment //join comment in exportdatacontext.Tblphotocomment on photo.Photoid equals comment.Photoid
                                             where photo.Photographerid == tbl.Photographerid
                                             select new Tblappointment()
                                             {
                                                 Appointmentid = photo.Appointmentid,
                                                 Slotid = photo.Slotid,
                                                 Slot=photo.Slot,
                                                 Photographerid = photo.Photographerid,
                                                 Customerid = photo.Customerid,
                                                 Appointmentdesc = photo.Appointmentdesc,
                                                 Statusid = photo.Statusid,
                                                 Status=photo.Status,
                                                 Customer=photo.Customer,
                                                 //Commentid=comment.Commentid,
                                                 //Customerid=comment.Customerid,
                                                 //Comment=comment.Comment,
                                                 //Commentdate=comment.Commentdate,
                                                
                                      }).ToList();
          
            return View(s);
        }

        // GET: Tblappointments/Details/5
        public async Task<IActionResult> Details()
        {
            var name = HttpContext.Session.GetString("cuname");
            var tblcust = await _context.Tblcustomer
                  .FirstOrDefaultAsync(m => m.Cuusername == name);



            IEnumerable<Tblappointment> s = (from photo in _context.Tblappointment //join comment in exportdatacontext.Tblphotocomment on photo.Photoid equals comment.Photoid
                                             where photo.Customerid == tblcust.Customerid
                                             select new Tblappointment()
                                             {
                                                 Appointmentid = photo.Appointmentid,
                                                 Slotid = photo.Slotid,
                                                 Slot = photo.Slot,
                                                 Photographerid = photo.Photographerid,
                                                 Customerid = photo.Customerid,
                                                 Appointmentdesc = photo.Appointmentdesc,
                                                 Statusid = photo.Statusid,
                                                 Status = photo.Status,
                                                 Photographer= photo.Photographer,
                                                 //Commentid=comment.Commentid,
                                                 //Customerid=comment.Customerid,
                                                 //Comment=comment.Comment,
                                                 //Commentdate=comment.Commentdate,

                                             }).ToList();

            return View(s);
        }
        public async Task<IActionResult> Detail()
        {

            return View();
        }
        // GET: Tblappointments/Create
        public async Task<IActionResult> Create(int? id)
        {
           

            IEnumerable<Tblappointmentslot> s = (from photo in _context.Tblappointmentslot //join comment in exportdatacontext.Tblphotocomment on photo.Photoid equals comment.Photoid
                                                 where photo.Photographerid == id
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

          

            ViewData["Photographerid"] = id;
         
          ViewData["Slotid"] = new SelectList(s, "Slotid", "Duration");
        
            return View();
        }
       
        // POST: Tblappointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Appointmentid,Slotid,Photographerid,Customerid,Appointmentdesc,Statusid")] Tblappointment tblappointment)
        {
            if (ModelState.IsValid)
            {
                var name = HttpContext.Session.GetString("cuname");
                var tblcust = await _context.Tblcustomer
                      .FirstOrDefaultAsync(m => m.Cuusername == name);
                var tbl = await _context.Tblphotographer
                     .FirstOrDefaultAsync(m => m.Photographerid == id);


                if (tblcust == null)
                {
                    return RedirectToAction(nameof(Index), "Tblphotographers");
                }
                tblappointment.Customerid = tblcust.Customerid;
                tblappointment.Statusid = 1;

                MailMessage mm = new MailMessage();
                mm.To.Add(tbl.Phmail);
                mm.Subject = "Book Appointment";
                mm.Body = "Check Your Appointment to approve or decline Request. ";
                mm.From = new MailAddress("kishan.vala.it.27@gmail.com");
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("kishan.vala.it.27@gmail.com", "9081010313");
                smtp.Send(mm);
                _context.Add(tblappointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Tblphotographers");
            }
            ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid", tblappointment.Customerid);
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblappointment.Photographerid);
            ViewData["Slotid"] = new SelectList(_context.Tblappointmentslot, "Slotid", "Slotid", tblappointment.Slotid);
            ViewData["Statusid"] = new SelectList(_context.Tblappointmentstatus, "Statusid", "Statusid", tblappointment.Statusid);
            return View(tblappointment);
        }

        // GET: Tblappointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointment = await _context.Tblappointment.FindAsync(id);
            if (tblappointment == null)
            {
                return NotFound();
            }
            ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid", tblappointment.Customerid);
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblappointment.Photographerid);
            ViewData["Slotid"] = new SelectList(_context.Tblappointmentslot, "Slotid", "Slotid", tblappointment.Slotid);
            ViewData["Statusid"] = new SelectList(_context.Tblappointmentstatus, "Statusid", "Statusid", tblappointment.Statusid);
            return View(tblappointment);
        }

        // POST: Tblappointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edi( [Bind("Appointmentid,Slotid,Photographerid,Customerid,Appointmentdesc,Statusid")] Tblappointment tblappointment)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    int cid=Convert.ToInt32( HttpContext.Request.Form["Customerid"]);
                    int aid = Convert.ToInt32(HttpContext.Request.Form["Appointmentid"]);
                    int pid = Convert.ToInt32(HttpContext.Request.Form["Photographerid"]);
                    int sid = Convert.ToInt32(HttpContext.Request.Form["Slotid"]);
                    tblappointment.Appointmentid =aid;
                    tblappointment.Photographerid = pid;
                    tblappointment.Customerid = cid;
                    tblappointment.Slotid = sid;
                    tblappointment.Appointmentdesc = HttpContext.Request.Form["Appointmentdesc"];
                    tblappointment.Statusid = 2;
                    var cd = cid;
                    var tblcust = await _context.Tblcustomer
                          .FirstOrDefaultAsync(m => m.Customerid == cd);

                    MailMessage mm = new MailMessage();
                    mm.To.Add(tblcust.Cumail);
                    mm.Subject = "Approve Appointment";
                    mm.Body = "Thank You.For Hire Me.Meet You soon.";
                    mm.From = new MailAddress("kishan.vala.it.27@gmail.com");
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("kishan.vala.it.27@gmail.com", "9081010313");
                    smtp.Send(mm);
                    _context.Update(tblappointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblappointmentExists(tblappointment.Appointmentid))
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
            ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid", tblappointment.Customerid);
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblappointment.Photographerid);
            ViewData["Slotid"] = new SelectList(_context.Tblappointmentslot, "Slotid", "Slotid", tblappointment.Slotid);
            ViewData["Statusid"] = new SelectList(_context.Tblappointmentstatus, "Statusid", "Statusid", tblappointment.Statusid);
            return View(tblappointment);
        }
        public async Task<IActionResult> Ed([Bind("Appointmentid,Slotid,Photographerid,Customerid,Appointmentdesc,Statusid")] Tblappointment tblappointment)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int cid = Convert.ToInt32(HttpContext.Request.Form["Customerid"]);
                    int aid = Convert.ToInt32(HttpContext.Request.Form["Appointmentid"]);
                    int pid = Convert.ToInt32(HttpContext.Request.Form["Photographerid"]);
                    int sid = Convert.ToInt32(HttpContext.Request.Form["Slotid"]);
                    tblappointment.Appointmentid = aid;
                    tblappointment.Photographerid = pid;
                    tblappointment.Customerid = cid;
                    tblappointment.Slotid = sid;
                    tblappointment.Appointmentdesc = HttpContext.Request.Form["Appointmentdesc"];
                    tblappointment.Statusid = 1002;
                    var cd = cid;
                    var tblcust = await _context.Tblcustomer
                          .FirstOrDefaultAsync(m => m.Customerid == cd);

                    MailMessage mm = new MailMessage();
                    mm.To.Add(tblcust.Cumail);
                    mm.Subject = " Cancel Appointment";
                    mm.Body = "Sorry I can't Accept your deal. ";
                    mm.From = new MailAddress("kishan.vala.it.27@gmail.com");
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("kishan.vala.it.27@gmail.com", "9081010313");
                    smtp.Send(mm);
                    _context.Update(tblappointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblappointmentExists(tblappointment.Appointmentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Tblphotographers");
            }
            ViewData["Customerid"] = new SelectList(_context.Tblcustomer, "Customerid", "Customerid", tblappointment.Customerid);
            ViewData["Photographerid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblappointment.Photographerid);
            ViewData["Slotid"] = new SelectList(_context.Tblappointmentslot, "Slotid", "Slotid", tblappointment.Slotid);
            ViewData["Statusid"] = new SelectList(_context.Tblappointmentstatus, "Statusid", "Statusid", tblappointment.Statusid);
            return View(tblappointment);
        }

        // GET: Tblappointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblappointment = await _context.Tblappointment
                .Include(t => t.Customer)
                .Include(t => t.Photographer)
                .Include(t => t.Slot)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.Appointmentid == id);
            if (tblappointment == null)
            {
                return NotFound();
            }

            return View(tblappointment);
        }

        // POST: Tblappointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblappointment = await _context.Tblappointment.FindAsync(id);
            _context.Tblappointment.Remove(tblappointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblappointmentExists(int id)
        {
            return _context.Tblappointment.Any(e => e.Appointmentid == id);
        }
    }
}
