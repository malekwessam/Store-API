using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Storee.Entities;
using Storee.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Get
{
	public class OrderViewModel:AbstractValidatableObject
	{
		public int Id { get; set; }
		public string CustomerName { get; set; }
		[DataType(DataType.EmailAddress)]
		public string CustomerEmail { get; set; }
		[DataType(DataType.PhoneNumber)]
		public string CustomerPhoneNumber { get; set; }
		public string? CustomerCity { get; set; }
		[DataType(DataType.PostalCode)]
		public string? postalcode { get; set; }
		
		public string? CustomerAddress { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal TotalAmount { get; set; }
		public string? OrderStatus { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal ShippingCost { get; set; }

		public List<OrderAndOrderItemViewModel> OrderAndOrderItemViewModels { get; set; }
	}
}
