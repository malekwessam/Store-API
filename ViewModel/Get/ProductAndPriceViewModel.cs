using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Get
{
    public class ProductAndPriceViewModel
    {
        public int Id { get; set; }
		public string? enCurrencyName { get; set; }
		public string? arCurrencyName { get; set; }
		public int? CountryId { get; set; }
		
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
	}
}
