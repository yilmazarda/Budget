using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.Data;
using Budget.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Budget.Controllers
{
    [Route("transactions")]
    public class TransactionsController : Controller
    {
        private readonly MyDbContext _context;

        public TransactionsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: transactions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.Include(t => t.Category).ToListAsync();
            return View(transactions);
        }

        // GET: transactions/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(ViewBag.Categories);
        }

        // POST: transactions/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(TransactionDto transactionDto)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the Category from the database
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == transactionDto.CategoryId);

                // If the category doesn't exist, return a 404 error
                if (category == null)
                {
                    return NotFound($"Category with ID {transactionDto.CategoryId} not found");
                }

                var transaction = new Transaction
                {
                    Name = transactionDto.Name,
                    Date = transactionDto.Date,
                    Value = transactionDto.Value,
                    CategoryId = transactionDto.CategoryId,
                    Category = category    
                };

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(transactionDto);
        }

        // GET: transactions/edit/5
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _context.Categories.ToList();
            var categoryTransactionModel = new  CategoryTransactionModel
            {Categories = ViewBag.Categories,
            Transaction = transaction};
            return View(categoryTransactionModel);
        }

        // POST: transactions/edit/5
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, TransactionDto transactionDto)
        {
            if (id != transactionDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var transaction = await _context.Transactions.FindAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }

                // Retrieve the Category from the database
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == transactionDto.CategoryId);

                // If the category doesn't exist, return a 404 error
                if (category == null)
                {
                    return NotFound($"Category with ID {transactionDto.CategoryId} not found");
                }

                transaction.Name = transactionDto.Name;
                transaction.Date = transactionDto.Date;
                transaction.Value = transactionDto.Value;
                transaction.CategoryId = transactionDto.CategoryId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(transactionDto);
        }

        // GET: transactions/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: transactions/delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
