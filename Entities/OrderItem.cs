﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storee.Entities
{
	public class OrderItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int? OrderId { get; set; }
		public int? ProductId { get; set; }
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPrice { get; set; }

		public Order Order { get; set; }
		public Product Product { get; set; }
	}
}
