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
    public class GenresController : Controller
    {
        private readonly ApplicationContext _context;

        public GenresController(ApplicationContext context)
        {
            _context = context;
        }

        #region GET ************************************************************************************************************************************************
        // GET: Display list of Genres
        public async Task<IActionResult> Index()
        {
            return _context.Genres != null ?
                        View("GenresIndex", await _context.Genres.ToListAsync()) :
                        Problem("Entity set 'ApplicationContext.Genres'  is null.");
        }

        // GET: Display create page for a Genre
        public IActionResult Create()
        {
            return View();
        }

        // GET: Display edit page for a Genre
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // GET: Display delete page for a Genre
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }
        #endregion


        #region POST ************************************************************************************************************************************************
        // POST: Create Genre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }
        #endregion


        #region PUT ************************************************************************************************************************************************
        // PUT: Edit Genre
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenreId,Name")] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreId))
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
            return View(genre);
        }
        #endregion


        #region DELETE ************************************************************************************************************************************************
        // DELETE: Delete Genre
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'ApplicationContext.Genres'  is null.");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        private bool GenreExists(int id)
        {
          return (_context.Genres?.Any(e => e.GenreId == id)).GetValueOrDefault();
        }

        public IActionResult Back()
        {
            return RedirectToAction("AdminIndex", "Admin");
        }
    }
}
