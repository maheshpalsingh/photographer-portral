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
using WebApplication5.ViewModels;


namespace WebApplication5.Controllers
{
    public class TblpackagesController : Controller
    {
        private readonly exportdataContext _context;

        public TblpackagesController(exportdataContext context)
        {
            _context = context;
        }

        // GET: Tblpackages
        public async Task<IActionResult> Index()
        {
            var name = HttpContext.Session.GetString("puname");
            var tbl = await _context.Tblphotographer
                  .FirstOrDefaultAsync(m => m.Phusername == name);

            IEnumerable<Tblpackage> s = (from photo in _context.Tblpackage //join comment in exportdatacontext.Tblphotocomment on photo.Photoid equals comment.Photoid
                                                 where photo.Phototgrapherid == tbl.Photographerid
                                                 select new Tblpackage()
                                                 {
                                                    Packageid=photo.Packageid,
                                                    Price=photo.Price,
                                                    Phototgrapherid=photo.Phototgrapherid,
                                                    Clicks=photo.Clicks,
                                                     //Commentid=comment.Commentid,
                                                     //Customerid=comment.Customerid,
                                                     //Comment=comment.Comment,
                                                     //Commentdate=comment.Commentdate,

                                                 }).ToList();

            return View(s);
        }

        // GET: Tblpackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblpackage = await _context.Tblpackage
                .Include(t => t.Phototgrapher)
                .FirstOrDefaultAsync(m => m.Packageid == id);
            if (tblpackage == null)
            {
                return NotFound();
            }

            return View(tblpackage);
        }

        // GET: Tblpackages/Create
        public IActionResult Create()
        {
         
            return View();
        }

        // POST: Tblpackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Packageid,Price,Phototgrapherid,Clicks")] Tblpackage tblpackage)
        {
            if (ModelState.IsValid)
            {
                var name = HttpContext.Session.GetString("puname");
                var tbl1 = await _context.Tblphotographer
                      .FirstOrDefaultAsync(m => m.Phusername == name);

                tblpackage.Phototgrapherid = tbl1.Photographerid;
                _context.Add(tblpackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Tblphotographers");
            }
          
            return View(tblpackage);
        }

        // GET: Tblpackages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblpackage = await _context.Tblpackage.FindAsync(id);
            if (tblpackage == null)
            {
                return NotFound();
            }
            ViewData["Phototgrapherid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblpackage.Phototgrapherid);
            return View(tblpackage);
        }

        // POST: Tblpackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Packageid,Price,Phototgrapherid,Clicks")] Tblpackage tblpackage)
        {
            if (id != tblpackage.Packageid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblpackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblpackageExists(tblpackage.Packageid))
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
            ViewData["Phototgrapherid"] = new SelectList(_context.Tblphotographer, "Photographerid", "Photographerid", tblpackage.Phototgrapherid);
            return View(tblpackage);
        }

        // GET: Tblpackages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblpackage = await _context.Tblpackage
                .Include(t => t.Phototgrapher)
                .FirstOrDefaultAsync(m => m.Packageid == id);
            if (tblpackage == null)
            {
                return NotFound();
            }

            return View(tblpackage);
        }

        // POST: Tblpackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblpackage = await _context.Tblpackage.FindAsync(id);
            _context.Tblpackage.Remove(tblpackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> moreinfo(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photographer = await _context.Tblpackage
                .Include(t =>t.Phototgrapher)
                .FirstOrDefaultAsync(m => m.Phototgrapherid == id);
          
            if (photographer == null)
            {
                return NotFound();
            }
         

            return View(photographer);
        }
        public async Task<IActionResult> Crea(int pid, int pgrid, int rating, string commentarea)
        {
            var name = HttpContext.Session.GetString("cuname");
            var tbl1 = await _context.Tblcustomer
                  .FirstOrDefaultAsync(m => m.Cuusername == name);
            Tblphotocomment tpc = new Tblphotocomment();
            tpc.Comment = commentarea;
            tpc.Customerid =tbl1.Customerid;
            tpc.Photoid = pid;
            _context.Add(tpc);
            await _context.SaveChangesAsync();

            Tblphotographerreview tpr = new Tblphotographerreview();
            tpr.Customerid = tbl1.Customerid;
            tpr.Photographerid = pgrid;
            tpr.Review = rating;
            _context.Add(tpr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Inde), new { id = tpr.Photographerid });
        }
        public ActionResult ShowComment(int phid)
        {
            IEnumerable<PhotoCommentViewModel> tblcomments = (from photo in _context.Tblphoto
                                                              join comment in _context.Tblphotocomment on photo.Photoid equals comment.Photoid
                                                              join customer in _context.Tblcustomer on comment.Customerid equals customer.Customerid
                                                              where photo.Photoid == phid
                                                              select new PhotoCommentViewModel()
                                                              {
                                                                  Photoid = photo.Photoid,
                                                                  //Photourl = photo.Photourl,
                                                                  //Photographerid = photo.Photographerid,
                                                                  //Categoryid = photo.Categoryid,
                                                                  Cuusername = customer.Cuusername,
                                                                 Customername = customer.Customername,
                                                                  Commentid = comment.Commentid,
                                                                  Customerid = comment.Customerid,
                                                                  Comment = comment.Comment,
                                                                 // Commentdate = comment.Commentdate,

                                                              }).ToList();
            return View(tblcomments);
        }

        public IActionResult Inde(int? id)
        {
            IEnumerable<PhotoCommentViewModel> tblphotoscomment = (from photo in _context.Tblphoto //join comment in exportdatacontext.Tblphotocomment on photo.Photoid equals comment.Photoid
                                                                  where photo.Photographerid==id
                                                                   select new PhotoCommentViewModel()
                                                                   {
                                                                       Photoid = photo.Photoid,
                                                                       Photourl = photo.Photourl,
                                                                       Photographerid = photo.Photographerid,
                                                                       Categoryid = photo.Categoryid,
                                                                       //Commentid=comment.Commentid,
                                                                       //Customerid=comment.Customerid,
                                                                       //Comment=comment.Comment,
                                                                       //Commentdate=comment.Commentdate,

                                                                   }).ToList();
            return View(tblphotoscomment);
        }
        private bool TblpackageExists(int id)
        {
            return _context.Tblpackage.Any(e => e.Packageid == id);
        }
    }
}
