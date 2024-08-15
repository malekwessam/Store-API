using System.Collections.Generic;

namespace Storee.ViewModel.Get
{
	public class HomeViewModel
	{
		public short Id { get; set; }
		public string arTitle { get; set; }
		public string enTitle { get; set; }
		public string arLargeTitle { get; set; }
		public string enLargeTitle { get; set; }

		public List<HomeProductViewModel> HomeProductViewModels { get; set; }
	}
}
