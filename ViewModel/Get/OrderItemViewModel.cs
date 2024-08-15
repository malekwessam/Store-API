using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Storee.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Get
{
	public class OrderItemViewModel:AbstractValidatableObject
	{
		public int Id { get; set; }
		public int? OrderId { get; set; }
		public int? ProductId { get; set; }
		public int Quantity { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal UnitPrice { get; set; }
	}
}
