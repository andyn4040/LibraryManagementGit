using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;

namespace Library_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationContext _context;

        public BooksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.Books != null ? 
                          View("BooksIndex", await _context.Books.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Books'  is null.");
        }

        // GET: Books/Details/5
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

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Books/Edit/5
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

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Books/Delete/5
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

        // POST: Books/Delete/5
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

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }

        // GET: Books/Search
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
                else if (selectedProperty == "GenreId")
                {
                    IQueryable<Genre> queryGenre = _context.Genres;

                    // Filter by GenreName
                    queryGenre = queryGenre.Where(g => g.Name.ToLower().Contains(lowerSearchTerm));

                    // Join the Book and Genre entities
                    query = query.Join(
                        queryGenre,
                        b => b.GenreId,
                        g => g.GenreId,
                        (b, g) => b
                    );
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
