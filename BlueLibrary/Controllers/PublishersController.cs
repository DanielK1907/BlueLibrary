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
    public class PublishersController : Controller
    {
        private readonly BlueLibraryContext _context;

        public PublishersController(BlueLibraryContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Publisher.ToListAsync());
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                String newPublisherName = publisher.Name.ToLower().Trim();
                if (_context.Genre.FirstOrDefault(p =>
                    p.Name.ToLower().Trim().Equals(newPublisherName)) != null)
                {
                    ViewData["Error"] = "Publisher with exact same name already exists";
                    return View(publisher);
                }

                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Publisher publisher)
        {
            if (id != publisher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                String newPublisherName = publisher.Name.ToLower().Trim();
                if (_context.Genre.FirstOrDefault(p =>
                    p.Name.ToLower().Trim().Equals(newPublisherName) && p.Id != publisher.Id) != null)
                {
                    ViewData["Error"] = "Publisher with exact same name already exists";
                    return View(publisher);
                }

                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.Id))
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
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publisher.FindAsync(id);
            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publisher.Any(e => e.Id == id);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PublisherGenres()
        {
            var publisherGenres = _context.Publisher
                .Select(p => new
                {
                    name = p.Name,
                    value = (_context.Book
                    .Where(b => b.PublisherId == p.Id)
                    .OrderBy(b => b.Genres.Count)
                    .Select(b => b.Genres.Count)
                    .Last()
                    )
                });

            var publisherWithMostGenres = await publisherGenres.ToListAsync();
            return Ok(publisherWithMostGenres);
        }
    }
}