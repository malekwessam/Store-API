using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Get
{
	public class CountryProductPriceViewModel
	{
		public int Id { get; set; }
		public int? CountryId { get; set; }
		public int? ProductId { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
	}
}
