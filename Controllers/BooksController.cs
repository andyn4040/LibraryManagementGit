using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Library_Management_System.Services;

namespace Library_Management_System.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ISessionService _sessionService;

        public BooksController(ApplicationContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        #region GET ************************************************************************************************************************************************
        // GET: Display list of Books
        public async Task<IActionResult> Index()
        {
            return _context.Books != null ?
                        View("BooksIndex", await _context.Books.ToListAsync()) :
                        Problem("Entity set 'ApplicationContext.Books'  is null.");
        }

        // GET: Display details for a single book
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Display create page for a Book
        public IActionResult Create()
        {
            return View();
        }

        // GET: Display edit page for a Book
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Display delete page for a Book
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Search for a Book
        public async Task<IActionResult> Search(string searchTerm, string selectedProperty)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Display all books if no search term provided
                return View("BooksIndex", await _context.Books.ToListAsync());
            }

            var lowerSearchTerm = searchTerm.ToLower();

            IQueryable<Book> query = _context.Books;

            if (!string.IsNullOrEmpty(selectedProperty))
            {
                if (selectedProperty == "ISBNumber" && int.TryParse(searchTerm, out var isbn))
                {
                    // Special handling for ISBN as an integer
                    query = query.Where(b => EF.Property<int>(b, selectedProperty) == isbn);
                }
                else if (selectedProperty == "Genre")
                {
                    IQueryable<BookGenre> queryGenre = _context.BookGenres;

                    query = from book in _context.Books
                            where book.BookGenres.Any(bg => bg.Genre.Name == lowerSearchTerm)
                            select book;
                }
                else
                {
                    // Use reflection to dynamically filter by the selected property
                    query = query.Where(b => EF.Property<string>(b, selectedProperty).ToLower().Contains(lowerSearchTerm));
                }
            }
            else
            {
                // Perform a case-insensitive search based on title or ISBN
                query = query.Where(b => b.Name.ToLower().Contains(lowerSearchTerm) || b.ISBNumber.ToString().Contains(searchTerm));
            }

            var searchResults = await query.ToListAsync();

            return View("BooksIndex", searchResults);
        }

        //GET: Route to Transactions page for Book
        public async Task<IActionResult> Checkout(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (transaction == null)
            {
                return RedirectToAction("Create", "Transactions", new { id = id });
                //return NotFound();
            }

            return RedirectToAction("", "Transactions");
        }
        #endregion


        #region POST ************************************************************************************************************************************************
        // POST: Create Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Name,AuthorId,ISBNumber,Summary,Available,Pages,GenreId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        #endregion


        #region PUT ************************************************************************************************************************************************
        // PUT: Edit Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Name,AuthorId,ISBNumber,Summary,Available,Pages,GenreId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            return View(book);
        }
        #endregion


        #region DELETE ************************************************************************************************************************************************
        // DELETE: Delete Book
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'ApplicationContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }

        public IActionResult Home()
        {
            return RedirectToAction("", "");
        }

        public IActionResult Back()
        {
            return RedirectToAction("AdminIndex", "Admin");
        }
    }
}
