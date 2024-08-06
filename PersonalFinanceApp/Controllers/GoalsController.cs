using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Controllers
{
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Goals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Goals.ToListAsync());
        }

        // GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // GET: Goals/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(ExpenseCategory)).Cast<ExpenseCategory>()
                                     .Select(e => new { ID = e, Name = e.ToString() }), "ID", "Name");
            return View();
        }

        // POST: Goals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,GoalAmount")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                goal.SpentAmount = await CalculateSpentAmount(goal.Category);
                goal.Balance = goal.GoalAmount - goal.SpentAmount;

                _context.Add(goal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(ExpenseCategory)).Cast<ExpenseCategory>()
                                     .Select(e => new { ID = e, Name = e.ToString() }), "ID", "Name");
            return View(goal);
        }

        // GET: Goals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(ExpenseCategory)).Cast<ExpenseCategory>()
                                     .Select(e => new { ID = e, Name = e.ToString() }), "ID", "Name");
            return View(goal);
        }

        // POST: Goals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,GoalAmount")] Goal goal)
        {
            if (id != goal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    goal.SpentAmount = await CalculateSpentAmount(goal.Category);
                    goal.Balance = goal.GoalAmount - goal.SpentAmount;

                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalExists(goal.Id))
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
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(ExpenseCategory)).Cast<ExpenseCategory>()
                                     .Select(e => new { ID = e, Name = e.ToString() }), "ID", "Name");
            return View(goal);
        }

        // GET: Goals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal != null)
            {
                _context.Goals.Remove(goal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }

        private async Task<decimal> CalculateSpentAmount(ExpenseCategory category)
        {
            return await _context.Expenses
                .Where(e => e.Category == category)
                .SumAsync(e => e.Value);
        }
    }
}
