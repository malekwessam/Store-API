using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storee.Entities
{
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEmail { get; set; }
		public string CustomerPhoneNumber { get; set; }
		public string? CustomerCity { get; set; }
		public string? postalcode { get; set; }
		public string? CustomerAddress { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalAmount { get; set; }
		public string? OrderStatus { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal ShippingCost { get; set; }
	

		public ICollection<OrderItem> OrderItem { get; set; }
		public ShoppingCart ShoppingCart { get; set; }
		public ICollection<Payment> Payment { get; set; }




	}
}
