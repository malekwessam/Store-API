using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.Entities
{
	public class CartItem
	{
		public int Id { get; set; }
		public int? ShoppingCartId { get; set; }
		public int? ProductId { get; set; }
		public int? Quantity { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal? UnitPrice { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal? TotalPrice { get; set; }

		// Navigation properties
		public ShoppingCart ShoppingCart { get; set; }
		public Product Product { get; set; }
	}
}
