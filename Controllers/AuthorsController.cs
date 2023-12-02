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
    public class AuthorsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ISessionService _sessionService;

        public AuthorsController(ApplicationContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        #region GET ************************************************************************************************************************************************
        // GET: Display list of Authors
        public async Task<IActionResult> Index()
        {
              return _context.Authors != null ? 
                          View("AuthorsIndex", await _context.Authors.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.Authors'  is null.");
        }

        // GET: Display create page
        public IActionResult Create()
        {
            return View();
        }

        // GET: Display edit page
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // GET: Display delete page
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
        #endregion


        #region POST ************************************************************************************************************************************************
        // POST: Create Author
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,FirstName,LastName")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        #endregion


        #region PUT ************************************************************************************************************************************************
        // PUT: Edit Author
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,FirstName,LastName")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
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
            return View(author);
        }
        #endregion


        #region DELETE ************************************************************************************************************************************************
        // POST: Delete Author
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Authors == null)
            {
                return Problem("Entity set 'ApplicationContext.Authors'  is null.");
            }
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        private bool AuthorExists(int id)
        {
          return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }

        public IActionResult Back()
        {
            return RedirectToAction("HomeIndex", "Home");
        }
    }
}
