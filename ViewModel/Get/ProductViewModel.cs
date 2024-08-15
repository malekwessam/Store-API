using Storee.Validation;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Storee.ViewModel.Get
{
    public class ProductViewModel : AbstractValidatableObject
    {
        public int Id { get; set; }
        [Required]
       
        public string NameEnglish { get; set; }
		[Required]

        public string NameArabic { get; set; }
        [Required]

        public string DescriptionsEnglish { get; set; }
		[Required]

        public string DescriptionsArabic { get; set; }
        [Required]
        public string pathImage { get; set; }
        public DateTime AvailableSince { get; set; }
        public bool IsActive { get; set; } = true;
        public short CategoryId { get; set; }
		//public string[] ImageUrls { get; set; }
		//public List<string> Sizes { get; set; }
		//public List<string> Colors { get; set; }

	}
}
