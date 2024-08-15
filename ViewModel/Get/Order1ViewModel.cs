﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storee.ViewModel.Get
{
	public class Order1ViewModel
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
	}
}
