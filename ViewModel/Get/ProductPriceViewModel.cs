using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Storee.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Get
{
	public class ProductPriceViewModel : AbstractValidatableObject
	{
		public int Id { get; set; }
		public int? ProductId { get; set; }
		public string? enCurrencyName { get; set; }
		public string? arCurrencyName { get; set; }
		public int? CountryId { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		
		public decimal Price { get; set; }
	}
}
