using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storee.Entities
{
	public class Country
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Currency { get; set; }
		public DateTime CreatedAt { get; set; }= DateTime.Now;
		[Required]
		[Column(TypeName = "nvarchar(max)")]
		public string PathImage { get; set; }

		public ICollection<ProductPrice> ProductPrice { get; set; }
	}
}
