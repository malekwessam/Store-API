using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storee.Entities
{
	public class ShoppingCart
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }= DateTime.Now;
		public string SessionId { get; set; }

		// Navigation properties
		public ICollection<Order> Order { get; set; }
		public ICollection<CartItem> CartItem { get; set; }
	}
}
