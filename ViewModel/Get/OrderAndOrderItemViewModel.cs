using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Get
{
	public class OrderAndOrderItemViewModel
	{
		public int Id { get; set; }
		public int? OrderId { get; set; }
		public int? ProductId { get; set; }
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPrice { get; set; }
	}
}
