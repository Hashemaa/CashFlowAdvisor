using CashFlowAdvisor.Data;
using CashFlowAdvisor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashFlowAdvisor.Controllers
{
	public class ExpensesController(ApplicationDbContext db) : Controller
	{
		private readonly ApplicationDbContext _db = db;
		public async Task<IActionResult> Index()
		{
			return View(await _db.Expenses.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Expense obj)
		{
			if (ModelState.IsValid)
			{
				obj.Created_at = DateTime.Now;
				await _db.Expenses.AddAsync(obj);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(obj);		
		}

		public async Task<IActionResult> Edit(int? id)
		{
			Expense? obj = await _db.Expenses.FindAsync(id);
			if (id != null && obj != null)
			{
				return View(obj);
			}
			return NotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Expense obj)
		{
			if (ModelState.IsValid)
			{
				_db.Expenses.Update(obj);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			Expense? obj = await _db.Expenses.FindAsync(id);
			if (id != null && obj != null)
			{
				return View(obj);
			}
			return NotFound();
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(int? id)
		{
			Expense? obj = await _db.Expenses.FindAsync(id);
			if (id != null && obj != null)
			{
				_db.Expenses.Remove(obj);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return NotFound();
		}
	}
}
