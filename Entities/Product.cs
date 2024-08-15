using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Storee.Entities
{
	public class Product
	{
		public Product()
		{
			Price=new List<ProductPrice>();
			OrderItem=new List<OrderItem>();
			
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[MinLength(3), MaxLength(250)]
		[Column(TypeName = "nvarchar(250)")]
		public string ProductName { get; set; }

		public string ProductNameAr { get; set; }

		[MinLength(1), MaxLength(8000)]
		[Column(TypeName = "nvarchar(max)")]
		public string? ProductDescription { get; set; }
		public string? ProductDescriptionAr { get; set; }

		public DateTime? AvailableSince { get; set; }
		public DateTime? CreatedDate { get; set; }
		
		public int NumberOfQuantity { get; set; }
		[Required]
		public int Quantity { get; set; }

		public bool? IsActive { get; set; } = true;
		
		public short? CategoryId { get; set; }
		public short? HomeId { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(max)")]
		public string PathImage { get; set; }

		
		
		public string[] ImageUrls { get; set; }
		public bool? IsNew { get; set; } 
		public decimal? SalePercentage { get; set; } // نسبة الخصم
		public bool? IsSale { get; set; } // حالة الخصم
		public Category Category { get; set; }
		public Home Home { get; set; }
		public ICollection<ProductPrice> Price { get; set; }
		public ICollection<OrderItem> OrderItem { get; set; }
		

		public List<string> Sizes { get; set; }
		public List<string> Colors { get; set; }
		
	}
}
