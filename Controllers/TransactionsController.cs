using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;
using Library_Management_System.ViewModels;

namespace Library_Management_System.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationContext _context;

        public TransactionsController(ApplicationContext context)
        {
            _context = context;
        }

        #region GET ************************************************************************************************************************************************
        // GET: Display list of Transactions
        public async Task<IActionResult> Index()
        {
            return _context.Transactions != null ?
                        View("TransactionsIndex", await _context.Transactions.ToListAsync()) :
                        Problem("Entity set 'ApplicationContext.Transactions'  is null.");
        }

        // GET: Display details for a single transaction
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Display create page for a Transaction
        public IActionResult Create(int id)
        {
            // Check if the book with the given id exists
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            var transactionViewModel = new TransactionCreateViewModel
            {
                BookId = id,
                BookName = book.Name, // Assuming there's a property named "Name" in your Book model
                BorrowDate = DateTime.UtcNow
            };

            _context.Transactions.Add(new Transaction
            {
                BookId = id,
                BorrowDate = DateTime.UtcNow
            });

            return View(transactionViewModel);
        }

        // GET: Display edit page for a Transaction
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // GET: Display delete page for a Transaction
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }
        #endregion


        #region POST ************************************************************************************************************************************************
        // POST: Create Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,UserId,BookId,BorrowDate,ReturnDate")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Update(transaction);
                var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == transaction.BookId);
                book.Available = false;
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }
        #endregion


        #region PUT ************************************************************************************************************************************************
        // PUT: Edit Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,UserId,BookId,BorrowDate,ReturnDate")] Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
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
            return View(transaction);
        }
        #endregion


        #region DELETE ************************************************************************************************************************************************
        // DELETE: Delete Transaction
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == transaction.BookId);
            book.Available = true;

            if (transaction != null && book != null)
            {
                _context.Books.Update(book);
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion


        private bool TransactionExists(int id)
        {
          return (_context.Transactions?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        }

        public IActionResult Back()
        {
            return RedirectToAction("AdminIndex", "Admin");
        }
    }
}
