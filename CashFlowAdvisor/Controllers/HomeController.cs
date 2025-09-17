using System.Diagnostics;
using CashFlowAdvisor.Data;
using CashFlowAdvisor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashFlowAdvisor.Controllers
{
    public class HomeController(ApplicationDbContext db, ILogger<HomeController> logger) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly ILogger<HomeController> _logger = logger;

        public async Task<IActionResult> Index()
        {
            List<Expense> expenses = await _db.Expenses.ToListAsync();
            List<IncomeSource> incomeSources = await _db.IncomeSources.ToListAsync();

            double totalExpenses = 0.00D;
            foreach (Expense expense in expenses)
                totalExpenses += expense.Price;

			double totalIncomeSources = 0.00D;
			foreach (IncomeSource incomeSource in incomeSources)
				totalIncomeSources += incomeSource.Price;

            double netCashFlow = totalIncomeSources - totalExpenses;

            var model = new CashFlowViewModel
            {
               NetCashFlow = netCashFlow,
            };

			return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
