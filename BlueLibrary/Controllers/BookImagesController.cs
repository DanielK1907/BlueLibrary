using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlueLibrary.Data;
using BlueLibrary.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlueLibrary.Controllers
{
    [Authorize]
    public class BookImagesController : Controller
    {
        private readonly BlueLibraryContext _context;

        public BookImagesController(BlueLibraryContext context)
        {
            _context = context;
        }

        // GET: BookImages
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookImage.ToListAsync());
        }

        // GET: BookImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookImage = await _context.BookImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookImage == null)
            {
                return NotFound();
            }

            return View(bookImage);
        }

        // GET: BookImages/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,ImageURL,ImageDescription")] BookImage bookImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookImage);
        }

        // GET: BookImages/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookImage = await _context.BookImage.FindAsync(id);
            if (bookImage == null)
            {
                return NotFound();
            }
            return View(bookImage);
        }

        // POST: BookImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageURL,ImageDescription")] BookImage bookImage)
        {
            if (id != bookImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookImageExists(bookImage.Id))
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
            return View(bookImage);
        }

        // GET: BookImages/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookImage = await _context.BookImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookImage == null)
            {
                return NotFound();
            }

            return View(bookImage);
        }

        // POST: BookImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookImage = await _context.BookImage.FindAsync(id);
            _context.BookImage.Remove(bookImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookImageExists(int id)
        {
            return _context.BookImage.Any(e => e.Id == id);
        }
    }
}
