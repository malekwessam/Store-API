using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Storee.ViewModel.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Get
{
    public class Product1ViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string NameEnglish { get; set; }
		[Required]
		[MinLength(5)]
		public string NameArabic { get; set; }
		[Required]

        public string DescriptionsEnglish { get; set; }
		[Required]

		public string DescriptionsArabic { get; set; }
		[Required]
        public string pathImage { get; set; }
        public DateTime AvailableSince { get; set; }
        public bool IsActive { get; set; } = true;
		public bool IsNew { get; set; } = true;
		public bool IsSale { get; set; } = true;
		[Column(TypeName = "decimal(18,2)")]
		public decimal? salePercentage { get; set; }
		public string[] ImageUrls { get; set; }
		public List<string> Colors { get; set; }
		public List<string> Sizes { get; set; }
		public short CategoryId { get; set; }
		//public short? HomeId { get; set; }

		public List<ProductAndPriceViewModel> ProductAndPriceViewModels { get; set; }
    }
}
