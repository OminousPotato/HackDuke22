using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SustainabotMVC.Models;

namespace SustainabotMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly SustainabotMVCContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;  

        public ReviewController(SustainabotMVCContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Review
        // search function 
        public async Task<IActionResult> Index(string searchString)
        {

            if (searchString == null || searchString.Length == 0) {
                return View("~/Views/Home/Index.cshtml");
            }

            var review = from m in _context.Review
                        select m;

            ViewData["Title"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                review = review.Where(s => s.Company!.Contains(searchString));
            }

            return View(await review.ToListAsync());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company,Product,Rating,Summary,ImgFile")] ReviewImgFile review)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(review);

                Review trueReview = new Review {
                    Company = review.Company,
                    Product = review.Product,
                    Rating = review.Rating,
                    Summary = review.Summary,
                    ImgPath = uniqueFileName,
                };

                _context.Add(trueReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReviewImgFile == null)
            {
                return NotFound();
            }

            var review = await _context.ReviewImgFile.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Company,Product,Rating,Summary,ImgPath")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
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
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReviewImgFile == null)
            {
                return NotFound();
            }

            var review = await _context.ReviewImgFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReviewImgFile == null)
            {
                return Problem("Entity set 'SustainabotMVCContext.Review'  is null.");
            }
            var review = await _context.ReviewImgFile.FindAsync(id);
            if (review != null)
            {
                _context.ReviewImgFile.Remove(review);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
          return (_context.ReviewImgFile?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string UploadedFile(ReviewImgFile model)  
        {  
            string uniqueFileName = null;  
  
            if (model.ImgFile != null)  
            {  
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImgFile.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.ImgFile.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
        }          

    }
}
