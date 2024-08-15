using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storee.Entities
{
	public class Category
	{
		public Category()
		{
			Product = new HashSet<Product>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public short Id { get; set; }
		[Required]
		[MinLength(4), MaxLength(100)]
		[Column(TypeName = "nvarchar(250)")]
		public string CategoryName { get; set; }
		public string CategoryNameAr { get; set; }

		public bool? IsAcTive { get; set; } = true;
		[Required]
		[Column(TypeName = "nvarchar(max)")]
		public string PathImage { get; set; }

		

		public virtual ICollection<Product> Product { get; set; }
	}
}

