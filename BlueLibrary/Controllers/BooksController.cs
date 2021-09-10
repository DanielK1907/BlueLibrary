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
using System.IO;
using TweetSharp;
using System.Net;

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

            ViewData["Genres"] = new SelectList(blueLibraryConext.Genre, "Id", "Name");

            return View(await allBooks.ToListAsync());
        }

        public async Task<IActionResult> Search(string title, string author, string publisherName, int? genreId, DateTime? startDate, DateTime? endDate)
        {
            var searchContext = blueLibraryConext.Book
                .Include(b => b.Image)
                .Include(b => b.Publisher)
                .Include(b => b.Genres)
                .Where(b => 
            (title == null || b.BookName.ToLower().Contains(title.Trim().ToLower())) &&
            (publisherName == null || (b.Publisher != null && b.Publisher.Name.ToLower().Contains(publisherName.Trim().ToLower()))) &&
            (genreId == null || b.Genres.Contains(blueLibraryConext.Genre.Find(genreId))) &&
            (author == null || b.Author.ToLower().Contains(author.Trim().ToLower())) &&
            (startDate == null || (b.ReleaseDate != null && b.ReleaseDate >= startDate)) &&
            (endDate == null || (b.ReleaseDate != null && b.ReleaseDate <= endDate)));

            ViewData["Genres"] = new SelectList(blueLibraryConext.Genre, "Id", "Name");
            return View("Watch", await searchContext.ToListAsync());
        }
        public async Task<IActionResult> Clear()
        {
            var searchContext = blueLibraryConext.Book
                .Include(b => b.Image)
                .Include(b => b.Publisher)
                .Include(b => b.Genres);

            ViewData["Genres"] = new SelectList(blueLibraryConext.Genre, "Id", "Name");
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
            ViewData["ImageId"] = new SelectList(
                blueLibraryConext.BookImage.Include(b => b.Book).Where(b => b.Book == null), "Id", "ImageURL");
            ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Name");
            ViewData["GenresIds"] = new MultiSelectList(blueLibraryConext.Genre, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,BookName,Author,ReleaseDate,Description,ImageId,PublisherId")] Book book, int[] genresIds)
        {
            if (ModelState.IsValid)
            {
                String newBookName = book.BookName.ToLower().Trim();
                if (blueLibraryConext.Book.FirstOrDefault(b =>
                    b.BookName.ToLower().Trim().Equals(newBookName)) != null)
                {
                    ViewData["ImageId"] = new SelectList(
                        blueLibraryConext.BookImage.Include(b => b.Book).Where(b => b.Book == null), "Id", "ImageURL");
                    ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Name");
                    ViewData["GenresIds"] = new MultiSelectList(blueLibraryConext.Genre, "Id", "Name");
                    ViewData["Error"] = "Book with exact same name already exists";
                    return View(book);
                }

                book.Genres = new List<Genre>();
                foreach (var genreId in genresIds)
                {
                    book.Genres.Add(blueLibraryConext.Genre.FirstOrDefault(g => g.Id == genreId));
                }
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

            var book = await blueLibraryConext.Book
                .Include(b => b.Image)
                .Include(b => b.Publisher)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(
                blueLibraryConext.BookImage.Include(b => b.Book).Where(b => b.Book == null || b.Book.Id == id), "Id", "ImageURL", book.ImageId);
            ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Name", book.PublisherId);
            ViewData["GenresIds"] = new MultiSelectList(blueLibraryConext.Genre, "Id", "Name");
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,Author,ReleaseDate,Description,ImageId,PublisherId")] Book book, int[] GenresIds)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                String newBookName = book.BookName.ToLower().Trim();
                if (blueLibraryConext.Book.FirstOrDefault(b =>
                    b.BookName.ToLower().Trim().Equals(newBookName) && b.Id != book.Id) != null)
                {
                    ViewData["ImageId"] = new SelectList(
                        blueLibraryConext.BookImage.Include(b => b.Book).Where(b => b.Book == null), "Id", "ImageURL");
                    ViewData["PublisherId"] = new SelectList(blueLibraryConext.Publisher, "Id", "Name");
                    ViewData["GenresIds"] = new MultiSelectList(blueLibraryConext.Genre, "Id", "Name");
                    ViewData["Error"] = "Book with exact same name already exists";
                    return View(book);
                }

                try
                {
                    blueLibraryConext.Book.Remove(book);

                    var updatedBook = new Book();
                    updatedBook.BookName = book.BookName;
                    updatedBook.Author = book.Author;
                    updatedBook.ReleaseDate = book.ReleaseDate;
                    updatedBook.Description = book.Description;
                    updatedBook.ImageId = book.ImageId;
                    updatedBook.PublisherId = book.PublisherId;
                    updatedBook.Publisher = book.Publisher;
                    updatedBook.Image = book.Image;

                    if (GenresIds.Length > 0)
                    {
                         updatedBook.Genres = new List<Genre>();

                        foreach (var genreId in GenresIds)
                        {
                            updatedBook.Genres.Add(blueLibraryConext.Genre.FirstOrDefault(g => g.Id == genreId));
                        }
                    }
                    else
                    {
                        updatedBook.Genres = book.Genres;
                    }

                    blueLibraryConext.Book.Add(updatedBook);

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

        [HttpPost]
        public async Task<IActionResult> PostOnTwitter(string bookName, string Image, string tweets)
        {
            string key = "G8eiLF0oqm66BMWSCdxvQOxpD";
            string secret = "X9jtSMahKw8hPe4b5Xj6ds39DKOLX9ufXaiEVDFvkCmVgKkfBS";
            string token = "1431627254837260290-1ESplU5nuBxZKTOrWbqUgMNfAwC76H";
            string tokenSecret = "CFm5gdzzKL0e0Ak1TkukVIMTkg4hThg6eklX3t0RU8JKN";


            var service = new TweetSharp.TwitterService(key, secret);
            service.AuthenticateWith(token, tokenSecret);

            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData("wwwroot/" + Image);

            if (bytes != null)
            {
                using (var stream = new MemoryStream(bytes))
                {
                    var tweetToPost = new SendTweetWithMediaOptions
                    {
                        Status = tweets,
                        Images = new Dictionary<string, Stream> { { "myPic", stream } }
                    };
                    var result = service.SendTweetWithMedia(tweetToPost);
                    if (result == null)
                    {
                        ViewData["Error"] = "Cannot post Tweet";
                    }
                }
            }
            else
            {
                var tweetToPost = new SendTweetOptions
                {
                    Status = tweets
                };
                var result = service.SendTweet(tweetToPost);
                if (result == null)
                {
                    ViewData["Error"] = "Cannot post Tweet";
                }
            }
            return RedirectToAction(nameof(Watch));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GenresWithMostBooks()
        {
            var booksGenres = blueLibraryConext.Genre.Include(g => g.Books).Select(g => new
            {
                name = g.Name,
                value = g.Books.Count
            });

            var booksList = await booksGenres.ToListAsync();
            return Ok(booksList);
        }
    }
}
