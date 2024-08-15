using Storee.Entities;
using System;
using System.Collections.Generic;

namespace Storee.ViewModel.Get
{
    public class CategoryProductViewModel
    {
        public int Id { get; set; }

        public string enProductName { get; set; }
		public string arProductName { get; set; }
		public string enDescription { get; set; }
		public string arDescription { get; set; }
		public string pathImage { get; set; }

        public DateTime AvailableSince { get; set; }
        public bool IsActive { get; set; }
		public bool IsNew { get; set; }
		public bool IsSale { get; set; }
        public decimal? SalePercentage { get; set; }

		public ICollection<ProductPriceViewModel> Price { get; set; }
	}
}
