using Storee.Validation;

namespace Storee.ViewModel.Get
{
	public class Country1ViewModel: AbstractValidatableObject
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string pathImage { get; set; }
	}
}
