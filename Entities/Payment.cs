using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.Entities
{
	public class Payment
	{
		public int Id { get; set; }
		public int? OrderId { get; set; }
		public string PaymentMethod { get; set; }
		public string PaymentIntentId { get; set; }
		public string PaymentStatus { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }
		public DateTime CreatedAt { get; set; }= DateTime.Now;
		public DateTime UpdatedAt { get; set; }
		public string Currency { get; set; }

		public Order Order { get; set; }
	}
}
