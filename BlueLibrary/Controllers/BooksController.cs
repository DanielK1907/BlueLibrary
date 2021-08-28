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
    public class BooksController : Controller
    {
        private readonly BlueLibraryContext blueLibraryConext;

        public BooksController(BlueLibraryContext context)
        {
            blueLibraryConext = context;
        }

        // GET: Books/Watch
        public async Task<IActionResult> Watch()
        {
            IQueryable<Book> allBooks = blueLibraryConext.Book
                .Include(b => b.Image)
                .Include(b => b.Publisher)
                .Include(b => b.Genres);

            return View(await allBooks.ToListAsync());
        }

        public async Task<IActionResult> Search(string title, string author, string publisherName)
        {
            var searchContext= blueLibraryConext.Book
                .Include(b => b.Image)
                .Include(b => b.Publisher)
                .Include(b => b.Genres)
                .Where(b => 
            (title == null || b.BookName.ToLower().Contains(title.Trim().ToLower())) &&
            (publisherName == null || (b.Publisher != null && b.Publisher.Name.ToLower().Contains(publisherName.Trim().ToLower()))) &&
            (author == null || b.Author.ToLower().Contains(author.Trim().ToLower())));

            return View("Watch", await searchContext.ToListAsync());
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var blueLibraryContext = blueLibraryConext.Book.Include(b => b.Image).Include(b => b.Publisher);
            return View(await blueLibraryContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await blueLibraryConext.Book
                .Include(b => b.Image)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(blueLibraryConext.BookImage, "Id", "ImageURL");
            ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,BookName,Author,ReleaseDate,Description,ImageId,PublisherId")] Book book)
        {
            if (ModelState.IsValid)
            {
                blueLibraryConext.Add(book);
                await blueLibraryConext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImageId"] = new SelectList(blueLibraryConext.BookImage, "Id", "Id", book.ImageId);
            ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Id", book.PublisherId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await blueLibraryConext.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(blueLibraryConext.BookImage, "Id", "ImageURL", book.ImageId);
            ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Name", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,Author,ReleaseDate,Description,ImageId,PublisherId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    blueLibraryConext.Update(book);
                    await blueLibraryConext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["ImageId"] = new SelectList(blueLibraryConext.BookImage, "Id", "Id", book.ImageId);
            ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Id", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await blueLibraryConext.Book
                .Include(b => b.Image)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await blueLibraryConext.Book.FindAsync(id);
            blueLibraryConext.Book.Remove(book);
            await blueLibraryConext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return blueLibraryConext.Book.Any(e => e.Id == id);
        }
    }
}
