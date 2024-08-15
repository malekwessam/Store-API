using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Storee.Entities
{
	public class ProductPrice
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int? CountryId { get; set; }
		public int? ProductId { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		
		public decimal Price { get; set; }
		public string? enCurrencyName { get; set; }
		public string? arCurrencyName { get; set; }
		public DateTime? CreatedDate { get; set; } = DateTime.Now;

		public Product Product { get; set; }
		public Country Country { get; set; }
	}
}
