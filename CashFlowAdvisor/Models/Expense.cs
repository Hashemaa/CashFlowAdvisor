using System.ComponentModel.DataAnnotations;

namespace CashFlowAdvisor.Models
{
	public class Expense
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; } = string.Empty;
		[Required]
		public double Price { get; set; } = 0.00D;
		public string? Description { get; set; } = null;
		[Required]
		public DateTime Created_at { get; set; } = DateTime.Now;
	}
}
