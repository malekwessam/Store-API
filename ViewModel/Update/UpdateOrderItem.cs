using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Storee.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Update
{
	public class UpdateOrderItem:AbstractValidatableObject
	{
		public int OrderItemId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
