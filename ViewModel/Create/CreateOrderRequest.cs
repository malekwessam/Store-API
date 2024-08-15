using Storee.Entities;
using Storee.ViewModel.Get;
using Storee.ViewModel.Update;
using System.Collections.Generic;

namespace Storee.ViewModel.Create
{
	public class CreateOrderRequest
	{
		public string CustomerName { get; set; }
		
		public string CustomerPhoneNumber { get; set; }
		public string? CustomerCity { get; set; }
		public string? postalcode { get; set; }
		public string? CustomerAddress { get; set; }

		public string CustomerEmail { get; set; }// بدلاً من CustomerId، استخدم معلومات بديلة مثل البريد الإلكتروني
		public List<OrderItemRequest> OrderItems { get; set; }
		public int CountryId { get; set; }
		public decimal ShippingCost { get; set; }
	}
}
