using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingApp.Areas.Identity.Data;
using VotingApp.Data;


namespace VotingApp.Controllers
{
    public class TVshowController : Controller
    {
        private readonly AdminDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<VoteAppUser> _userManager;
        
        public TVshowController(AdminDbContext context, IWebHostEnvironment hostEnvironment, UserManager<VoteAppUser> userManager)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: TVshows
        [Authorize]
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.TvShow.ToListAsync());
        }

        // GET: TVshows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVshow = await _context.TvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (tVshow == null)
            {
                return NotFound();
            }

            return View(tVshow);
        }

        // GET: TVshows/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TVshows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Id,ShowName,Reviewer,Review,Rating,ImageFile")] TVshow tVshow)
        {
            if (ModelState.IsValid)
            {

                //Reviewer name addition
                
                var userId = _userManager.GetUserId(HttpContext.User);
                VoteAppUser user = _userManager.FindByIdAsync(userId).Result;
                tVshow.Reviewer = user.Name;
                

                //image addition
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tVshow.ImageFile.FileName);
                string extention = Path.GetExtension(tVshow.ImageFile.FileName);
                tVshow.ShowImg=fileName = fileName + DateTime.Now.ToString("yymmssffff") + extention;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream=new FileStream(path, FileMode.Create))
                {
                    await tVshow.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(tVshow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tVshow);
        }
        
        // GET: TVshows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVshow = await _context.TvShow.FindAsync(id);
            //var show = (TVshow)_context.TvShow.Where(j => j.Id == id).FirstOrDefault();
            //var current = tVshow.Reviewer;
            if (tVshow == null)
            {
                return NotFound();
            }
            return View(tVshow);
        }

        // POST: TVshows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShowName,Reviewer,Review,Rating,ShowImg")] TVshow tVshow)
        {
            if (id != tVshow.Id)
            {
                return NotFound();
            }
            //var show = _context.TvShow.FirstOrDefaultAsync(m => m.Id == id);

            string current = tVshow.Reviewer;
            var userId = _userManager.GetUserId(HttpContext.User);
            VoteAppUser user = _userManager.FindByIdAsync(userId).Result;
            tVshow.Reviewer = user.Name;
           



            if (ModelState.IsValid)
                {
                    try
                    {
                      if (user.Name==current)
                      {
                       _context.Update(tVshow);
                      }
                         await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TVshowExists(tVshow.Id))
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
                return View(tVshow);
            //}
            //else
            //{
            //    return NotFound();
            //}
        }
        [Authorize]
        // GET: TVshows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tVshow = await _context.TvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tVshow == null)
            {
                return NotFound();
            }
           

            return View(tVshow);
        }

        // POST: TVshows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tVshow = await _context.TvShow.FindAsync(id);

            string current = tVshow.Reviewer;
            var userId = _userManager.GetUserId(HttpContext.User);
            VoteAppUser user = _userManager.FindByIdAsync(userId).Result;
            if (user.Name == current)
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", tVshow.ShowImg);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
                _context.TvShow.Remove(tVshow);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TVshowExists(int id)
        {
            return _context.TvShow.Any(e => e.Id == id);
        }
    }
}
