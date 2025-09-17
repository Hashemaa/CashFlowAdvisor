using CashFlowAdvisor.Data;
using CashFlowAdvisor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashFlowAdvisor.Controllers
{
	public class IncomeSourcesController(ApplicationDbContext db) : Controller
	{
		private readonly ApplicationDbContext _db = db;

		public async Task<IActionResult> Index()
		{
			List<IncomeSource> list = await _db.IncomeSources.ToListAsync();
			return View(list);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(IncomeSource obj)
		{
			if (ModelState.IsValid)
			{
				obj.Created_at = DateTime.Now;
				await _db.IncomeSources.AddAsync(obj);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			return View(await _db.IncomeSources.FirstOrDefaultAsync(x => x.Id == id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(IncomeSource obj)
		{
			if (ModelState.IsValid)
			{
				//add updated at date time
				IncomeSource? oldObj = await _db.IncomeSources.FirstOrDefaultAsync(x => x.Id == obj.Id);
				oldObj.Title = obj.Title;
				oldObj.Price = obj.Price;
				oldObj.Description = obj.Description;
				_db.IncomeSources.Update(oldObj);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			return View(await _db.IncomeSources.FindAsync(id));
		}

		[HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
		public async Task<IActionResult> DeletePost(int? id)
		{
			IncomeSource? obj = await _db.IncomeSources.FindAsync(id);
			if (id != null && obj != null)
			{
				_db.IncomeSources.Remove(obj);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return NotFound();
		}
	}
}
