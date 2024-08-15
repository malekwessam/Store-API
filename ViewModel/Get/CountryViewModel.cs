using Storee.Validation;
using System.Collections.Generic;

namespace Storee.ViewModel.Get
{
	public class CountryViewModel : AbstractValidatableObject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public string pathImage { get; set; }


		public List<CountryProductPriceViewModel> CountryProductPriceViewModels { get; set; }
	}
}
